using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Xsport.Common.Emuns;
using Xsport.Common.Utils;
using Xsport.DB.Entities;
using Xsport.Common.Models;
using Xsport.DTOs.UserDtos;
using Xsport.Common.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Xsport.Core.EmailServices.Models;
using Xsport.Core.EmailServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using FirebaseAdmin.Auth;
using Newtonsoft.Json.Linq;
using MimeKit.Encodings;
using Xsport.DB;
using Xsport.Common.Constants;
using Xsport.DB.QueryObjects;
using Xsport.Common.Enums;
using Xsport.Core.LoggerServices;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Xsport.Core;
public class UserServices : IUserServices
{
    private readonly AppDbContext _db;
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ILoggerManager _logger;
    private UserManager<XsportUser> userManager { get; }
    private JwtConfig JwtConfig { get; }
    GeneralConfig GeneralConfig { get; }
    private IEmailService emailService { get; }
    public IRepositoryManager _repositoryManager { get; set; }
    public UserServices(
        AppDbContext db,
        IWebHostEnvironment _webHostEnvironment,
        IHttpContextAccessor _httpContextAccessor,
        UserManager<XsportUser> _userManager,
        IOptionsMonitor<JwtConfig> _optionsMonitor,
        IOptionsMonitor<GeneralConfig> _optionsMonitor2,
        IEmailService _emailService,
        IRepositoryManager repositoryManager,
        ILoggerManager logger)
    {
        _db = db;
        webHostEnvironment = _webHostEnvironment;
        httpContextAccessor = _httpContextAccessor;
        userManager = _userManager;
        JwtConfig = _optionsMonitor.CurrentValue;
        GeneralConfig = _optionsMonitor2.CurrentValue;
        emailService = _emailService;
        _repositoryManager = repositoryManager;
        _logger = logger;
    }

    public async Task<AuthResult> Register(UserRegistrationDto user, short currentLanguageId)
    {
        try
        {
            if (user == null) throw new Exception(
                currentLanguageId == (short)LanguagesEnum.English ?
                UserServiceErrors.bad_request_data_en :
                UserServiceErrors.bad_request_data_ar);
            if (user.Email.IsNullOrEmpty()) throw new Exception(
                currentLanguageId == (short)LanguagesEnum.English ?
                UserServiceErrors.required_email_en :
                UserServiceErrors.required_email_ar);
            //check if user exists
            var existingUser = await userManager.FindByEmailAsync(user.Email);
            if (existingUser != null) throw new Exception(user.Email +
                ((currentLanguageId == (short)LanguagesEnum.English) ?
                UserServiceErrors.email_taken_en :
                UserServiceErrors.email_taken_ar));
            XsportUser xsportUser = new XsportUser()
            {
                Email = user.Email,
                UserName = user.Email,
                XsportName = user.Name,
                PhoneNumber = user.Phone,
                LoyaltyPoints = 0,
                ImagePath = "",
                AuthenticationProvider = "EmailPassword",
                RegistrationStatus = (short)RegistrationStatusEnum.registeredWithEmailAndPassword
            };
            var isCreated = await userManager.CreateAsync(xsportUser, user.Password);
            if (!isCreated.Succeeded)
            {
                string errorMessage = string.Empty;
                foreach (var item in isCreated.Errors)
                {
                    errorMessage = "__" + errorMessage + item.Description + "\n";
                }

                throw new Exception(errorMessage);
            }
            //try
            //{
            //    var message = new Message(
            //        new List<string>() { user.Email ?? string.Empty },
            //        "Account Confirmation", confirmationCode, null);
            //    await emailService.SendEmailAsync(message);
            //}
            //catch (Exception)
            //{
            //    throw new Exception("Confirmation email could not be sent. " +
            //        "Please, make sure you entered a valide email address");
            //}
            
            AuthResult jwtToken = await GenerateJwtToken(xsportUser, GeneralConfig?.EnableTwoFactor ?? false);
            return jwtToken;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public Task<short> AccountStatus(XsportUser user)
    {
        try
        {
            return Task.FromResult(user.RegistrationStatus);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<ResetPassworTokendDto> ForgotPassword(ForgotPasswordDto dto, short currentLanguageId)
    {
        try
        {
            var existingUser = await userManager.FindByEmailAsync(dto.Email) ??
                throw new Exception(currentLanguageId == (short)LanguagesEnum.English ?
                UserServiceErrors.user_does_not_exist_en :
                UserServiceErrors.user_does_not_exist_ar);
            string confirmationCode = GenerateEmailConfirmationCode();
            try
            {
                var message = new Message(
                    new List<string>() { dto.Email ?? string.Empty },
                    "Reset Password", confirmationCode, null);
                await emailService.SendEmailAsync(message);
            }
            catch (Exception)
            {
                throw new Exception(
                    currentLanguageId == (short)LanguagesEnum.English ?
                    UserServiceErrors.email_send_error_en :
                    UserServiceErrors.email_send_error_ar);
            }
            existingUser.ResetPasswordCode = confirmationCode;
            await _db.SaveChangesAsync();
            string resetPasswordToken = await userManager.GeneratePasswordResetTokenAsync(existingUser);
            return new ResetPassworTokendDto()
            {
                ResetPassworTokend = resetPasswordToken
            };
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> ResetPassword(ResetPasswordDto dto, short currentLanguageId)
    {
        try
        {
            var existingUser = await userManager.FindByEmailAsync(dto.Email) ??
                throw new Exception(currentLanguageId == (short)LanguagesEnum.English ?
                UserServiceErrors.user_does_not_exist_en :
                UserServiceErrors.user_does_not_exist_ar);
            if (dto.Code != existingUser.ResetPasswordCode)
                throw new Exception(
                    currentLanguageId == (short)LanguagesEnum.English ?
                    UserServiceErrors.Err_reset_password_code_en :
                    UserServiceErrors.Err_reset_password_code_ar);
            var isRest = await userManager.ResetPasswordAsync(
                existingUser, dto.ResetPassworToken, dto.NewPassword);
            if (!isRest.Succeeded)
            {
                string errorMessage = string.Empty;
                foreach (var item in isRest.Errors)
                {
                    errorMessage = "__" + errorMessage + item.Description + "\n";
                }

                throw new Exception(errorMessage);
            }
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    //public async Task<bool> ResendEmailConfirmationCode(UserLoginRequest dto)
    //{
    //    try
    //    {
    //        var oldEmailUser = await userManager.FindByEmailAsync(dto.Email);
    //        string code = string.Empty;
    //        if (oldEmailUser == null)
    //        {
    //            var newEmailUser = await _db.XsportUsers.SingleOrDefaultAsync(u => u.NewEmail == dto.Email) ??
    //                throw new Exception("User does not exist.");
    //            code = await SendActivationCode(dto.Email);
    //            if (code.IsNullOrEmpty()) throw new Exception("Confirmation code could not be generated.");
    //            newEmailUser.EmailConfirmationCode = code;
    //            await _db.SaveChangesAsync();
    //            return true;
    //        }
    //        code = await SendActivationCode(dto.Email);
    //        if (code.IsNullOrEmpty()) throw new Exception("Confirmation code could not be generated.");
    //        oldEmailUser.EmailConfirmationCode = code;
    //        await _db.SaveChangesAsync();
    //        return true;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}
    //public async Task<ConfirmUserEmailRespDto> ConfirmUserEmail(ConfirmEmailDto dto, short currentLanguageId)
    //{
    //    try
    //    {
    //        XsportUser? oldEmailUser = await userManager.FindByEmailAsync(dto.Email);
    //        bool isCorrect = false;
    //        var jwtToken = new AuthResult();
    //        if (oldEmailUser == null)
    //        {
    //            XsportUser? newEmailUser = await _db.XsportUsers.SingleOrDefaultAsync(u => u.NewEmail == dto.Email) ??
    //                throw new Exception("User does not exist.");
    //            isCorrect = await userManager.CheckPasswordAsync(newEmailUser, dto.Password);
    //            if (!isCorrect) throw new Exception("Operation can not be completed, wrong password");
    //            if (newEmailUser.EmailConfirmationCode != dto.Code) throw new Exception("Confirmation code is incorrect.");
    //            newEmailUser.Email = newEmailUser.NewEmail;
    //            newEmailUser.NormalizedEmail = newEmailUser.NewEmail?.ToUpperInvariant();
    //            newEmailUser.UserName = newEmailUser.Email;
    //            newEmailUser.NormalizedUserName = newEmailUser.NewEmail?.ToUpperInvariant();
    //            newEmailUser.NewEmail = null;
    //            await _db.SaveChangesAsync();
    //            jwtToken = await GenerateJwtToken(newEmailUser, GeneralConfig?.EnableTwoFactor ?? false);
    //            UserProfileDto userProfile = await GetUserProfile(newEmailUser.Id, currentLanguageId);
    //            return new ConfirmUserEmailRespDto()
    //            {
    //                AuthResult = jwtToken,
    //                Sports = null,
    //                UserProfile = userProfile
    //            };
    //        }
    //        isCorrect = await userManager.CheckPasswordAsync(oldEmailUser, dto.Password);
    //        if (!isCorrect) throw new Exception("Operation can not be completed, wrong password");
    //        if (oldEmailUser.EmailConfirmationCode != dto.Code) throw new Exception("Confirmation code is incorrect.");
    //        oldEmailUser.EmailConfirmed = true;
    //        await _db.SaveChangesAsync();
    //        jwtToken = await GenerateJwtToken(oldEmailUser, GeneralConfig?.EnableTwoFactor ?? false);
    //        List<SportDto> sports = _db.Sports.Select(s => new SportDto()
    //        {
    //            SportId = s.SportId,
    //            SportName = (s.SportTranslations
    //            .SingleOrDefault(t => t.LanguageId == currentLanguageId).Name) ?? string.Empty,
    //        }).ToList();
    //        return new ConfirmUserEmailRespDto()
    //        {
    //            AuthResult = jwtToken,
    //            Sports = sports,
    //            UserProfile = null
    //        };
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}
    public async Task<LoginResponseDto> LoginAsync(UserLoginRequest user, short currentLanguageId)
    {
        try
        {
            _logger.LogInfo($"User with email = ${user.Email} trying to login.");
            var existingUser = await userManager.FindByEmailAsync(user.Email) ??
                throw new Exception(currentLanguageId == (short)LanguagesEnum.English ?
                UserServiceErrors.user_does_not_exist_en :
                UserServiceErrors.user_does_not_exist_ar);
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
    public async Task<LoginResponseDto> GoogleLoginAsync(UserGoogleLoginDto dto, short currentLanguageId)
    {
        string Uid;
        try
        {
            FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(dto.FirebaseToken);
            Uid = decodedToken.Uid;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        try
        {
            if (dto.Email.IsNullOrEmpty()) throw new Exception(
                currentLanguageId == (short)LanguagesEnum.English ?
                UserServiceErrors.required_email_en :
                UserServiceErrors.required_email_ar);
            var existingUser = await _db.XsportUsers.SingleOrDefaultAsync(u => u.Uid == Uid);
            if (existingUser == null)
            {
                existingUser = await userManager.FindByEmailAsync(dto.Email);
                if (existingUser == null)
                {
                    XsportUser xsportUser = new XsportUser()
                    {
                        Uid = Uid,
                        Email = dto.Email,
                        EmailConfirmed = true,
                        UserName = dto.Email,
                        XsportName = dto.Name,
                        PhoneNumber = dto.Phone,
                        LoyaltyPoints = 0,
                        ImagePath = "",
                        AuthenticationProvider = "Google",
                        RegistrationStatus = (short)RegistrationStatusEnum.registredWithGoogle,
                    };
                    var isCreated = await userManager.CreateAsync(xsportUser);
                    if (!isCreated.Succeeded) throw new Exception("User could not be created successfully.");
                    var jwtToken = await GenerateJwtToken(xsportUser, GeneralConfig?.EnableTwoFactor ?? false);
                    return new LoginResponseDto
                    {
                        AuthResult = jwtToken,
                        UserProfile = null
                    };
                }
                else
                {
                    var jwtToken = await GenerateJwtToken(existingUser, GeneralConfig?.EnableTwoFactor ?? false);
                    var userProfile = await GetUserProfile(existingUser.Id, currentLanguageId);
                    return new LoginResponseDto
                    {
                        AuthResult = jwtToken,
                        UserProfile = userProfile
                    };
                }

            }
            else
            {
                var jwtToken = await GenerateJwtToken(existingUser, GeneralConfig?.EnableTwoFactor ?? false);
                var userProfile = await GetUserProfile(existingUser.Id, currentLanguageId);
                return new LoginResponseDto
                {
                    AuthResult = jwtToken,
                    UserProfile = userProfile
                };
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<AuthResult> CompleteGoogleRegistrationAsync(
        long uId, CompleteGoogleRegistrationDto dto, short currentLanguageId)
    {
        try
        {
            var existingUser = await _db.XsportUsers.SingleOrDefaultAsync(u => u.Id == uId) ?? throw new Exception(
                (currentLanguageId == (short)LanguagesEnum.English) ?
                UserServiceErrors.user_does_not_exist_en :
                UserServiceErrors.user_does_not_exist_ar);
            existingUser.XsportName = dto.Name;
            existingUser.PhoneNumber = dto.Phone;
            existingUser.RegistrationStatus = (short)RegistrationStatusEnum.comletedRegistrationWithGoogle;
            await _db.SaveChangesAsync();
            AuthResult jwtToken = await GenerateJwtToken(existingUser, GeneralConfig?.EnableTwoFactor ?? false);
            return jwtToken;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<UserProfileDto> UploadProfilePicture(long uId, UploadProfilePictureDto dto, short currentLanguageId)
    {
        try
        {
            XsportUser? user = await _db.XsportUsers.Where(u => u.Id == uId)
                .Include(u => u.UserSports)
                .FirstOrDefaultAsync() ??
                throw new Exception(
                    currentLanguageId == (short)LanguagesEnum.English ?
                    UserServiceErrors.user_does_not_exist_en :
                    UserServiceErrors.user_does_not_exist_ar);
            if (dto.File == null) throw new Exception(
                                    currentLanguageId == (short)LanguagesEnum.English ?
                                    UserServiceErrors.profilepicture_required_en :
                                    UserServiceErrors.profilepicture_required_ar);
            string img = await Utils.UploadImageFileAsync(dto.File, user.Id, webHostEnvironment);
            user.ImagePath = img;
            user.RegistrationStatus = (short)RegistrationStatusEnum.RegistrationFinished;
            await _db.SaveChangesAsync();
            return await GetUserProfile(uId, currentLanguageId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<UserProfileDto> SkipProfilePicture(long uId, short currentLanguageId)
    {
        try
        {
            XsportUser? user = await _db.XsportUsers.Where(u => u.Id == uId)
                .Include(u => u.UserSports)
                .FirstOrDefaultAsync() ??
                throw new Exception(
                    currentLanguageId == (short)LanguagesEnum.English ?
                    UserServiceErrors.user_does_not_exist_en :
                    UserServiceErrors.user_does_not_exist_ar);
            user.RegistrationStatus = (short)RegistrationStatusEnum.profilePictureSkipped;
            await _db.SaveChangesAsync();
            return await GetUserProfile(uId, currentLanguageId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<UserProfileDto> CompleteRegistration(long uId, CompleteRegistrationDto dto, short currentLanguageId)
    {
        try
        {
            XsportUser? user = await _db.XsportUsers.Where(u => u.Id == uId)
                .Include(u => u.UserSports)
                .FirstOrDefaultAsync() ??
                throw new Exception(
                    currentLanguageId == (short)LanguagesEnum.English ?
                    UserServiceErrors.user_does_not_exist_en :
                    UserServiceErrors.user_does_not_exist_ar);
            //var isCorrect = await userManager.CheckPasswordAsync(user, dto.Password);
            //if (!isCorrect) throw new Exception("Incorrect password.");
            string img = string.Empty;
            if (dto.File != null) img = await Utils.UploadImageFileAsync(dto.File, user.Id, webHostEnvironment);
            user.ImagePath = img;
            await _db.SaveChangesAsync();
            await AddFavoriteSportsToUser(user, dto.SportsIds);
            return await GetUserProfile(uId, currentLanguageId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<UserProfileDto> GetUserProfile(long uId, short currentLanguageId)
    {
        XsportUser? user = null;
        try
        {
            user = await _db.XsportUsers.Where(u => u.Id == uId)
            .Include(u => u.UserSports)
            .ThenInclude(userSport => userSport.UserSportPreferenceValues)
            .Include(u => u.UserSports)
            .ThenInclude(userSport => userSport.Sport)
            .ThenInclude(sport => sport.SportTranslations)
            .Include(u => u.UserSports)
            .ThenInclude(userSport => userSport.Sport)
            .ThenInclude(sport => sport.Levels)
            .ThenInclude(level => level.LevelTranslations)
            .Include(u => u.UserSports)
            .ThenInclude(userSport => userSport.Sport)
            .ThenInclude(sport => sport.SportPreferences)
            .ThenInclude(sportPrefence => sportPrefence.SportPreferenceTranslations)
            .Include(u => u.UserSports)
            .ThenInclude(userSport => userSport.Sport)
            .ThenInclude(sport => sport.SportPreferences)
            .ThenInclude(prefernce => prefernce.SportPreferenceValues)
            .ThenInclude(sportPreferenceValue => sportPreferenceValue.SportPreferenceValueTranslations)
            .Include(u => u.UserMatchs)
            .ThenInclude(userMatch => userMatch.Match)
            //.AsNoTracking()
            .FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        if (user == null) throw new Exception(
            currentLanguageId == (short)LanguagesEnum.English ?
            UserServiceErrors.user_does_not_exist_en :
            UserServiceErrors.user_does_not_exist_ar);
        //if (user.UserSports == null || user.UserSports.Count == 0) throw new Exception("Please, Add at least one Favorite Sport!");
        UserSport? userSport = user.UserSports?.Where(userSport => userSport.IsCurrentState)?.FirstOrDefault();
        if (userSport == null && user.UserSports?.Count > 0)
        {
            userSport = user.UserSports?.OrderBy(userSport => userSport.UserSportId).FirstOrDefault();
            userSport!.IsCurrentState = true;
            await _db.SaveChangesAsync();
        }
        Sport? currentSport = userSport?.Sport;
        int sportUserPoints = userSport?.Points ?? 0;
        var sportLevels = currentSport?.Levels;
        (string, int) levelInfo = CalculateUserLevel(sportLevels?.ToList() ?? new List<Level>(), sportUserPoints, currentLanguageId);
        string domainName = httpContextAccessor.HttpContext?.Request.Scheme + "://" + httpContextAccessor.HttpContext?.Request.Host.Value;
        return new UserProfileDto()
        {
            User = new UserInfoDto()
            {
                UserId = user.Id,
                Name = user.XsportName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                LoyaltyPoints = user.LoyaltyPoints,
                Longitude = user.Longitude,
                Latitude = user.Latitude,
                ImgURL = string.IsNullOrEmpty(user.ImagePath) ? "" : domainName + "/Images/" + user.ImagePath,
                AuthenticationProvider = user.AuthenticationProvider,
            },
            FavoriteSports = user.UserSports?.Select(userSport =>
            {
                (string, int) levelInfo = CalculateUserLevel(userSport.Sport?.Levels.ToList() ?? new List<Level>(), sportUserPoints, currentLanguageId);
                return new FavoriteSportDto()
                {
                    Id = userSport.SportId,
                    Name = userSport.Sport?.SportTranslations?.First(t => t.LanguageId == currentLanguageId)?.Name ?? string.Empty,
                    IsCurrentState = userSport.IsCurrentState,
                    Points = userSport.Points,
                    NumOfMatchs = user.UserMatchs?
                    .Where(userMatch => userMatch.Match.SportId == userSport!.SportId)?
                    .ToList().Count ?? 0,
                    UserLevel = levelInfo.Item1,
                    LevelPercent = levelInfo.Item2,
                    Levels = userSport.Sport?.Levels.Select(sl => new SportLevel()
                    {
                        LevelName = sl.LevelTranslations.SingleOrDefault(t => t.LanguageId == currentLanguageId)?.Name ?? string.Empty,
                        LevelMaxPoints = sl.MaxPoints
                    }).ToList() ?? new List<SportLevel>(),
                    Preferences = userSport.Sport!.SportPreferences.Select(preference => new SportPreferenceDto()
                    {
                        SportPreferenceId = preference.SportPreferenceId,
                        SportPreferenceName = preference.SportPreferenceTranslations
                        .First(t => t.LanguageId == currentLanguageId)?.Name ?? string.Empty,
                        SportPreferenceValues = preference.SportPreferenceValues.Select(value => new SportPreferenceValueDto()
                        {
                            SportPreferenceValueId = value.SportPreferenceValueId,
                            SportPreferenceValueName = value.SportPreferenceValueTranslations?
                            .First(t => t.LanguageId == currentLanguageId)?
                            .Name ?? string.Empty
                        }).ToList(),
                        SelectedPreferenceValueId = userSport.UserSportPreferenceValues
                        .First(v => preference.SportPreferenceValues
                        .Select(v => v.SportPreferenceValueId).Contains(v.SportPreferenceValueId))
                        .SportPreferenceValueId
                    }).ToList(),
                };
            }).ToList(),
            Followers = 0,
            Following = 0
        };

    }
    public async Task<UserProfileDto> EditUserProfile(long uId, EditUserReqDto dto, short currentLanguageId)
    {
        try
        {
            XsportUser user = await _db.XsportUsers.Include(u => u.UserSports)
                .SingleOrDefaultAsync(u => u.Id == uId) ??
                throw new Exception("User does not exist");
            if (dto.SportsIds == null) throw new Exception("Please, choose at least one sport.");
            if (dto.SportsIds?.Count == 0) throw new Exception("Please, choose at least one sport.");
            List<long> existingSportsIds = user.UserSports.Select(us => us.SportId).ToList();
            List<long> toBeDeletedSportsIds = existingSportsIds.Except(dto.SportsIds).ToList();
            List<long> toBeAddedSportsIds = dto.SportsIds.Except(existingSportsIds).ToList();
            if (toBeDeletedSportsIds.Count != 0) await DeleteFavoriteSportsToUser(uId, toBeDeletedSportsIds, true);
            if (toBeAddedSportsIds.Count != 0) await AddFavoriteSportsToUser(user, toBeAddedSportsIds);
            //TODO delete old file.
            string img = user.ImagePath ?? string.Empty;
            if (dto.File != null) img = await Utils.UploadImageFileAsync(dto.File, user.Id, webHostEnvironment);
            user.XsportName = dto.Name;
            user.PhoneNumber = dto.Phone;
            user.Latitude = dto.Latitude;
            user.Longitude = dto.Longitude;
            user.ImagePath = img;
            await _db.SaveChangesAsync();
            return await GetUserProfile(uId, currentLanguageId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> ChangePassword(XsportUser user, ChangePasswordDto dto)
    {
        try
        {
            var result = await userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);
            if (!result.Succeeded)
                throw new Exception(result.Errors.First().Description);
            return result.Succeeded;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> ChangeEmail(XsportUser user, ChangeEmailDto dto, short currentLanguageId)
    {
        try
        {
            var existingUser = await userManager.FindByEmailAsync(dto.NewEmail);
            if (existingUser != null)
                throw new Exception(
                    currentLanguageId == (short)LanguagesEnum.English ?
                    UserServiceErrors.email_taken_en :
                    UserServiceErrors.email_taken_ar);
            user.Email = dto.NewEmail;
            user.NormalizedEmail = dto.NewEmail.ToUpperInvariant();
            user.UserName = dto.NewEmail;
            user.NormalizedUserName = dto.NewEmail.ToUpperInvariant();
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<bool> DeleteAccount(long uId)
    {
        try
        {
            XsportUser user = _db.XsportUsers
                .Include(u => u.UserSports)
                .ThenInclude(us => us.UserSportPreferenceValues)
                .SingleOrDefault(u => u.Id == uId) ??
                throw new Exception("User does not exist");
            List<long> SportsIds = user.UserSports.Select(us => us.SportId).ToList();
            await DeleteFavoriteSportsToUser(uId, SportsIds, true);
            _db.XsportUsers.Remove(user);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<UserProfileDto> AddFavoriteSports(AddFavoriteSportReqDto dto, long userId, short currentLangId)
    {
        try
        {
            var user = await _db.XsportUsers
                .Include(u => u.UserSports)
                .SingleOrDefaultAsync(u => u.Id == userId) ??
                throw new Exception("User does not exist");
            await AddFavoriteSportsToUser(user, dto.SportsIds);
            return await GetUserProfile(userId, currentLangId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    public async Task<UserProfileDto> DeleteFavoriteSports(AddFavoriteSportReqDto dto, long userId, short currentLangId)
    {
        try
        {
            await DeleteFavoriteSportsToUser(userId, dto.SportsIds, false);
            return await GetUserProfile(userId, currentLangId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    public async Task<UserProfileDto> EditPreferences(EditPreferencesReqDto dto, long userId, short currentLanguageId)
    {
        try
        {
            var user = await _db.XsportUsers
                .Include(u => u.UserSports)
                .ThenInclude(userSport => userSport.UserSportPreferenceValues)
                .SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null) throw new Exception("User does not exist.");
            if (user.UserSports == null) throw new Exception("Please, choose a favorite sport.");
            if (user.UserSports.Count == 0) throw new Exception("Please, choose a favorite sport.");
            var userSport = user.UserSports.SingleOrDefault(userSport => userSport.IsCurrentState);
            if (userSport == null) throw new Exception("Please, select a sport");
            var preference = await _db.SportPreferences
                .Include(sp => sp.SportPreferenceValues)
                .SingleOrDefaultAsync(p => p.SportPreferenceId == dto.PreferenceId);
            if (preference == null) throw new Exception("Preference does not exist.");
            var preferenceVlauesIds = preference.SportPreferenceValues.Select(v => v.SportPreferenceValueId).ToList();
            var old_US_PV = userSport.UserSportPreferenceValues.SingleOrDefault(v => preferenceVlauesIds.Contains(v.SportPreferenceValueId));
            if (old_US_PV == null) throw new Exception("Wrong while editing preference!");
            _db.UserSportPreferenceValues.Remove(old_US_PV);
            var value = _db.SportPreferenceValues.SingleOrDefault(v => v.SportPreferenceValueId == dto.ValueId);
            userSport.UserSportPreferenceValues.Add(new UserSportPreferenceValue()
            {
                UserSportId = userSport.UserSportId,
                SportPreferenceValueId = dto.ValueId,
                UserSport = userSport,
                SportPreferenceValue = value
            });
            await _db.SaveChangesAsync();
            return await GetUserProfile(userId, currentLanguageId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<UserProfileDto> SelectCurrentSport(long uId, long sportId, short currentLanguageId)
    {
        try
        {
            var user = _db.XsportUsers.Where(u => u.Id == uId)
                .Include(u => u.UserSports)
                .FirstOrDefault() ??
                throw new Exception("User Does not exist");
            if (user.UserSports == null)
                throw new Exception("User does not have favorite sports");
            if (user.UserSports.Count == 0)
                throw new Exception("User does not have favorite sports");
            if (user.UserSports.FirstOrDefault(us => us.SportId == sportId) == null)
                throw new Exception("The sport is not a favorite for the user.");
            foreach (var us in user.UserSports)
            {
                if (us.SportId == sportId)
                {
                    us.IsCurrentState = true;
                }
                else
                {
                    us.IsCurrentState = false;
                }
            }
            await _db.SaveChangesAsync();
            return await GetUserProfile(uId, currentLanguageId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<List<PlayersRankingListDto>> GetPlayers(long uId, GetPlayersReqDto dto, short currentLanguageId)
    {
        try
        {
            string domainName = httpContextAccessor.HttpContext?.Request.Scheme + "://" + httpContextAccessor.HttpContext?.Request.Host.Value;
            var user = await _repositoryManager.UserRepository.FindByConditionWithEagerLoad(false,
                u => u.Id == uId,
                u => u.UserSports).FirstAsync();
            if (user == null) throw new Exception("User does not exist.");
            if (user.UserSports == null) throw new Exception("You Must have at least one favorite sport.");
            if (user.UserSports.Count == 0) throw new Exception("You Must have at least one favorite sport.");
            var currentSport = user.UserSports.Where(us => us.IsCurrentState).FirstOrDefault();
            if (currentSport == null) throw new Exception("Please, Select a sport.");
            var sportId = currentSport.SportId;
            var players = await _repositoryManager.UserRepository.GetPlayersRankingList(
                sportId,
                currentLanguageId,
                PlayersRankingListOrderOptions.ByPointsDes,
                PlayersRankingListFilterOptions.ByPlayerName,
                dto.Name ?? string.Empty,
                dto.PageInfo.PageNumber,
                dto.PageInfo.PageSize, domainName);
            return players.Where(p => Utils.CalculateDistanceBetweenTowUsers(
                user.Latitude ?? 0, user.Longitude ?? 0, p.Lat, p.Long)
            <= XsportConstants.SameAreaRaduis).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<bool> InrollUserInCourse(InrollUserInCourseDto dto)
    {
        try
        {
            XsportUser user = await _db.XsportUsers.SingleOrDefaultAsync(u => u.Id == dto.UId) ??
                throw new Exception("User does not exist.");
            Course course = await _db.Courses.SingleOrDefaultAsync(_ => _.CourseId == dto.CourseId) ??
                throw new Exception("Course does not exist.");
            DateOnly todyDate = DateOnly.FromDateTime(DateTime.UtcNow);
            if (course.EndDate <= todyDate)
                throw new Exception("You can not be inrolled in a course that was already finished.");
            if (!dto.IsPersonal)
            {
                if (dto.RelativeId == null || dto.RelativeId == 0)
                    throw new Exception("Please, Provide valide Relative");
                Relative relative = await _repositoryManager.RelativeRepository
                    .FindByCondition(r => r.RelativeId == dto.RelativeId, false)
                    .SingleOrDefaultAsync() ?? throw new Exception("Relative does not exist.");
                UserCourse? existentUserCourse = await _repositoryManager.UserCourseRepository
                    .FindByCondition(
                    uc => uc.XsportUserId == user.Id &&
                    uc.CourseId == dto.CourseId &&
                    uc.RelativeId == dto.RelativeId &&
                    uc.IsPersonal == false, false)
                    .SingleOrDefaultAsync();
                if (existentUserCourse != null) throw new Exception("You are already subscribed to this course.");
            }
            else
            {
                if (dto.RelativeId != null && dto.RelativeId != 0) throw new Exception("Invalide Inputs.");
                UserCourse? existentUserCourse = await _repositoryManager.UserCourseRepository
                    .FindByCondition(
                    uc => uc.XsportUserId == user.Id &&
                    uc.CourseId == dto.CourseId &&
                    uc.IsPersonal == true, false)
                    .SingleOrDefaultAsync();
                if (existentUserCourse != null) throw new Exception("You are already subscribed to this course.");
            }
            UserCourse userCourse = new UserCourse()
            {
                CourseId = dto.CourseId,
                XsportUserId = user.Id,
                RelativeId = (dto.RelativeId == 0) ? null : dto.RelativeId,
                Name = dto.Name,
                Phone = dto.Phone,
                ResidencePlace = dto.ResidencePlace,
                Points = 0,
                IsPersonal = dto.IsPersonal
            };
            await _db.UserCourses.AddAsync(userCourse);
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<bool> AddAcademyReview(long uId, AddAcademyReviewDto dto)
    {
        try
        {
            Academy academy = await _repositoryManager.AcademyRepository
                .FindByCondition(a => a.AcademyId == dto.AcademyId, false)
                .SingleOrDefaultAsync() ?? throw new Exception("Academy does not exist.");
            DateTime reviewDateTime = DateTime.UtcNow;
            AcademyReview review = new AcademyReview()
            {
                AcademyId = dto.AcademyId,
                XsportUserId = uId,
                Description = dto.ReviewText,
                Evaluation = dto.Evaluation,
                ReviewDateTime = reviewDateTime,
            };
            await _repositoryManager.AcademyReviewRepository.CreateAsync(review);
            await _repositoryManager.AcademyReviewRepository.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
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

    private async Task AddFavoriteSportsToUser(XsportUser user, List<long> sportsIds)
    {
        try
        {
            var sports = await _db.Sports.Where(s => sportsIds.Contains(s.SportId))
            .Include(s => s.SportPreferences)
            .ThenInclude(p => p.SportPreferenceValues)
            .ToListAsync();
            if (sports == null)
                throw new Exception("Sports do not exist.");
            if (sports.Count == 0 || sports.Count != sportsIds.Count)
                throw new Exception("Sports do not exist.");
            var favoriteSportsIds = user.UserSports.Select(us => us.SportId).ToList();
            foreach (var sport in sports)
            {
                if (!favoriteSportsIds.Contains(sport.SportId))
                {
                    var notAssignedValuesIds = sport.SportPreferences
                        .Select(p =>
                        p.SportPreferenceValues.SingleOrDefault(v => v.IsNotAssigned ?? false)?.SportPreferenceValueId)
                        .ToList();
                    var newUserSport = new UserSport()
                    {
                        XsportUser = user,
                        Sport = sport,
                        IsCurrentState = false,
                        Points = 0,
                        UserSportPreferenceValues = notAssignedValuesIds.Select(id => new UserSportPreferenceValue()
                        {
                            SportPreferenceValueId = id ?? 0
                        }).ToList()
                    };
                    user.UserSports.Add(newUserSport);
                }
            }
            await _db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private async Task DeleteFavoriteSportsToUser(long uId, List<long> sportsIds, bool deleteOrEditAccount)
    {
        try
        {
            var user = await _db.XsportUsers
                .Include(u => u.UserSports)
                .ThenInclude(us => us.UserSportPreferenceValues)
                .SingleAsync(u => u.Id == uId);
            if (user == null) throw new Exception("User does not exist.");
            if ((sportsIds.Count >= user.UserSports.Count) && !deleteOrEditAccount)
                throw new Exception("Sorry, but You have to keep at least one favorite sport!");
            var userSports = user.UserSports.Where(us => sportsIds.Contains(us.SportId)).ToList();
            foreach (var userSport in userSports)
            {
                //TODO
                foreach (var us_pv in userSport.UserSportPreferenceValues)
                {
                    _db.UserSportPreferenceValues.Remove(us_pv);
                }
                user.UserSports.Remove(userSport);

            }

            // Save changes
            await _db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<AuthResult> GenerateJwtToken(XsportUser user, bool enableTwoFactor)
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
    private async Task<string> SendActivationCode(string userEmail)
    {
        try
        {
            string code = GenerateEmailConfirmationCode();
            var message = new Message(new List<string>() { userEmail ?? string.Empty }, "Account Confirmation", code, null);
            await emailService.SendEmailAsync(message);
            return code;
        }
        catch (Exception)
        {
            throw new Exception("Confirmation email could not be sent. Please, make sure you entered a valide email address");
        }
    }
    public string GenerateEmailConfirmationCode()
    {
        try
        {
            Random random = new Random();
            int randomNumber = random.Next(100000, 999999);
            string code = randomNumber.ToString("D6");
            return code;
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