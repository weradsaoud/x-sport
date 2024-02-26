using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Xsport.Common.Emuns;
using Xsport.Common.Utils;
using Xsport.Db;
using Xsport.DB.Entities;
using Xsport.Common.Models;
using Xsport.DTOs.UserDtos;
using Xsport.Common.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Xsport.Core.EmailServices.Models;
using Xsport.Core.EmailServices;

namespace Xsport.Core;
public class UserServices : IUserServices
{
    private readonly AppDbContext _db;
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly IHttpContextAccessor httpContextAccessor;
    private UserManager<XsportUser> userManager { get; }
    private JwtConfig JwtConfig { get; }
    GeneralConfig GeneralConfig { get; }
    private IEmailService emailService { get; }
    public UserServices(AppDbContext db, IWebHostEnvironment _webHostEnvironment, IHttpContextAccessor _httpContextAccessor, UserManager<XsportUser> _userManager, IOptionsMonitor<JwtConfig> _optionsMonitor, IOptionsMonitor<GeneralConfig> _optionsMonitor2, IEmailService _emailService)
    {
        _db = db;
        webHostEnvironment = _webHostEnvironment;
        httpContextAccessor = _httpContextAccessor;
        userManager = _userManager;
        JwtConfig = _optionsMonitor.CurrentValue;
        GeneralConfig = _optionsMonitor2.CurrentValue;
        emailService = _emailService;
    }

    public async Task<bool> Register(UserRegistrationDto user, short currentLanguageId)
    {
        if (user == null) throw new Exception(UserServiceErrors.bad_request_data);
        XsportUser xsportUser = new XsportUser()
        {
            Email = user.Email,
            UserName = user.Name,
            PhoneNumber = user.Phone,
            LoyaltyPoints = 0,
            Latitude = user.Latitude,
            Longitude = user.Longitude,
            ImagePath = ""
        };
        var isCreated = await userManager.CreateAsync(xsportUser, user.Password);
        if (isCreated.Succeeded)
        {
            //send verification e-mail
            await SendActivationLink(xsportUser);
            //return registration is successful.
            return true;
            //try
            //{
            //    var authResult = await GenerateJwtToken(xsportUser, GeneralConfig?.EnableTwoFactor ?? false);
            //    var sports = _db.Sports.Select(sport => new SportDto()
            //    {
            //        SportId = sport.SportId,
            //        Name = sport.SportTranslations
            //        .Where(t => t != null)
            //        .Where(t => t.LanguageId == currentLanguageId)
            //        .First().Name ?? string.Empty
            //    }).ToList();
            //    return new RegisterResponseDto
            //    {
            //        Sports = sports,
            //        AuthResult = authResult
            //    };
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
        }
        else
        {
            string errorMessage = string.Empty;
            foreach (var item in isCreated.Errors)
            {
                errorMessage = "__" + errorMessage + item.Description + "\n";
            }

            throw new Exception(errorMessage);
        }

    }
    public async Task<bool> ConfirmUserEmail(string userId, string token)
    {
        var user = await userManager.FindByIdAsync(userId) ?? throw new Exception("User does not exist");

        // Confirm the email using the token
        var result = await userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded) throw new Exception("Invalid confirmation token");
        return true;
    }
    public async Task<LoginResponseDto> LoginAsync(UserLoginRequest user, short currentLanguageId)
    {
        try
        {
            var existingUser = await userManager.FindByEmailAsync(user.Email) ?? throw new Exception("User does not exist");
            var isLockout = await userManager.IsLockedOutAsync(existingUser);
            if (isLockout)
                throw new Exception("User is locked out");
            var isCorrect = await userManager.CheckPasswordAsync(existingUser, user.Password);
            if (!isCorrect)
            {
                // increase the failed log in count
                if (userManager.SupportsUserLockout && userManager.GetLockoutEnabledAsync(existingUser).Result)
                {
                    await userManager.AccessFailedAsync(existingUser);
                }
                throw new Exception("Invalid authentication");
            }
            //if (GeneralConfig?.EnableTwoFactor ?? false)
            //{
            //    if (await userManager.GetTwoFactorEnabledAsync(existingUser))
            //        return await GenerateOTPFor2StepVerification(existingUser, ai);
            //}

            var jwtToken = await GenerateJwtToken(existingUser, GeneralConfig?.EnableTwoFactor ?? false);

            // if log in success then reset the failed log in count to zero
            if (userManager.SupportsUserLockout && userManager.GetAccessFailedCountAsync(existingUser).Result > 0)
            {
                await userManager.ResetAccessFailedCountAsync(existingUser);
            }
            var userProfile = await GetUserProfile(existingUser.Id, currentLanguageId);
            return new LoginResponseDto
            {
                AuthResult = jwtToken,
                UserProfile = userProfile,
            };
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<UserProfileDto> CompleteRegistration(CompleteRegistrationDto dto, long uId, short currentLanguageId)
    {
        XsportUser? user = await _db.XsportUsers.Where(u => u.Id == uId).FirstOrDefaultAsync() ??
        throw new Exception(UserServiceErrors.user_does_not_exist);

        string img = string.Empty;
        if (dto.File != null) img = await Utils.UploadImageFileAsync(dto.File, user.Id, webHostEnvironment);
        user.ImagePath = img;
        await _db.SaveChangesAsync();
        await AddFavoriteSportsToUser(uId, dto.SportsIds);
        return await GetUserProfile(uId, currentLanguageId);
    }
    public async Task<UserProfileDto> GetUserProfile(long uId, short currentLanguageId)
    {
        XsportUser? user = null;
        try
        {
            user = await _db.XsportUsers.Where(u => u.Id == uId)
            .Include(u => u.UserSports)
            .ThenInclude(userSport => userSport.Sport)
            .ThenInclude(sport => sport.Levels)
            .ThenInclude(level => level.LevelTranslations)
            .Include(u => u.UserSports)
            .ThenInclude(userSport => userSport.UserSportPreferences)
            .ThenInclude(userSportPreference => userSportPreference.SportPreference)
            .ThenInclude(sportPreference => sportPreference.SportPreferenceTranslations)
            .Include(u => u.UserSports)
            .ThenInclude(userSport => userSport.UserSportPreferences)
            .ThenInclude(userSportPreference => userSportPreference.SportPreferenceValue)
            .Include(u => u.UserMatchs)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        if (user == null) throw new Exception(UserServiceErrors.user_does_not_exist);
        UserSport? userSport = user.UserSports?.Where(userSport => userSport.IsCurrentState)?.FirstOrDefault();
        Sport? currentSport = userSport?.Sport;
        int sportUserPoints = userSport?.Points ?? 0;
        var sportLevels = currentSport?.Levels;
        (string, int) levelInfo = CalculateUserLevel(sportLevels?.ToList() ?? new List<Level>(), sportUserPoints, currentLanguageId);
        List<CurrentSportPreference>? _currentSportPreferences = userSport?.UserSportPreferences?.Select(userSportPreference => new CurrentSportPreference()
        {
            SportPreferenceId = userSportPreference.SportPreferenceId,
            SportPreferenceName = userSportPreference.SportPreference.SportPreferenceTranslations
            .Where(t => t.LanguageId == currentLanguageId).FirstOrDefault()?.Name ?? string.Empty,
            SportPreferenceValueId = userSportPreference.SportPreferenceValueId,
            SportPreferenceValue = userSportPreference.SportPreferenceValue?.Name ?? string.Empty
        })?.ToList();
        List<CurrentSportPreference>? currentSportPreferences = 
        string domainName = httpContextAccessor.HttpContext?.Request.Scheme + "://" + httpContextAccessor.HttpContext?.Request.Host.Value;
        return new UserProfileDto()
        {
            User = new UserInfo()
            {
                UserId = user.Id,
                Name = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                LoyaltyPoints = user.LoyaltyPoints,
                Longitude = user.Longitude,
                Latitude = user.Latitude,
                ImgURL = string.IsNullOrEmpty(user.ImagePath) ? "" : domainName + "/Images/" + user.ImagePath
            },
            FavoriteSports = user.UserSports?.Select(userSport => new FavoriteSport()
            {
                Id = userSport.SportId,
                Name = userSport.Sport?.Name ?? string.Empty,
                IsCurrentState = userSport.IsCurrentState
            }).ToList(),
            CurrentSport = new CurrentSport()
            {
                CurrentSportId = currentSport?.SportId ?? user.UserSports?.FirstOrDefault()?.SportId ?? 0,
                NumOfMatchs = user.UserMatchs?.Count ?? 0,
                Points = sportUserPoints,
                UserLevel = levelInfo.Item1,
                LevelPercent = levelInfo.Item2,
                Preferences = currentSportPreferences
            }
        };

    }
    private (string, int) CalculateUserLevel(List<Level> levels, int userPoints, short currentLanguageId)
    {
        if (levels == null || levels.Count == 0) return (string.Empty, 0);
        foreach (Level level in levels.OrderBy(level => level.MaxPoints))
        {
            if (userPoints < level.MaxPoints)
                return (level.LevelTranslations.FirstOrDefault(t => t.LanguageId == currentLanguageId)?.Name ?? string.Empty, 100 * userPoints / level.MaxPoints);
        }
        return (string.Empty, 0);
    }

    private async Task AddFavoriteSportsToUser(long uId, List<long> sportsIds)
    {
        var user = await _db.XsportUsers.Include(u => u.UserSports).SingleAsync(u => u.Id == uId);
        var sports = await _db.Sports.Where(s => sportsIds.Contains(s.SportId)).ToListAsync();
        foreach (var sport in sports)
        {
            user.UserSports.Add(new UserSport()
            {
                XsportUser = user,
                Sport = sport,
                IsCurrentState = false,
                Points = 0,
            });
        }

        // Save changes
        await _db.SaveChangesAsync();
    }

    private async Task<AuthResult> GenerateJwtToken(XsportUser user, bool enableTwoFactor)
    {
        try
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(JwtConfig.Secret);
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName ?? string.Empty));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = JwtConfig.Audience,
                Issuer = JwtConfig.Issuer,
                IssuedAt = DateTime.UtcNow,
                Subject = new ClaimsIdentity(
                    claims.ToArray()),
                Expires = DateTime.UtcNow.AddMinutes(JwtConfig.ExpiryInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);
            string newToken = RandomString(35) + Guid.NewGuid();
            var refreshToken = new RefreshToken()
            {
                JwtId = user.Id.ToString(),
                IsUsed = false,
                IsRevoked = false,
                UserId = user.Id,
                AddedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(JwtConfig.RefreshTokenExpiryInDays),// DateTime.UtcNow.AddMonths(1),
                Token = newToken
            };
            //TODO should deal with repository not directly with dbcontext
            await _db.RefreshTokens.AddAsync(refreshToken);
            await _db.SaveChangesAsync();

            return new AuthResult()
            {
                Token = jwtToken,
                Success = true,
                RefreshToken = newToken, //refreshToken.Token,
                Is2StepVerificationRequired = enableTwoFactor,
                Provider = "Email"
            };
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    private async Task SendActivationLink(XsportUser user)
    {
        try
        {
            //var existingUser = await UserManager.FindByIdAsync(userId.ToString());
            if (user == null) throw new Exception("Invalid user");
            var request = httpContextAccessor.HttpContext?.Request ?? throw new Exception("Domain could not be retrieved");
            var domain = $"{request.Scheme}://{request.Host}{request.PathBase}";
            var emailConfirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
            string activationLink = $"{domain}/api/User/ConfirmUserEmail?userId={user.Id}&token={System.Web.HttpUtility.UrlEncode(emailConfirmationToken)}";
            var message = new Message(new List<string>() { user.Email ?? string.Empty }, "Account Confirmation", activationLink, null);
            await emailService.SendEmailAsync(message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    private string RandomString(int length)
    {
        var random = new Random();
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(x => x[random.Next(x.Length)]).ToArray());
    }
}