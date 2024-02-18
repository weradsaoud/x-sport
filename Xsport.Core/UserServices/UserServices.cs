using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Xsport.Common.Emuns;
using Xsport.Common.Utils;
using Xsport.Db;
using Xsport.DB.Entities;
using Xsport.DTOs.UserDtos;

namespace Xsport.Core;
public class UserServices : IUserServices
{
    private readonly AppDbContext _db;
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly IHttpContextAccessor httpContextAccessor;
    public UserServices(AppDbContext db, IWebHostEnvironment _webHostEnvironment, IHttpContextAccessor _httpContextAccessor)
    {
        _db = db;
        webHostEnvironment = _webHostEnvironment;
        httpContextAccessor = _httpContextAccessor;
    }

    public async Task<UserProfileDto> CompleteRegistration(CompleteRegistrationDto dto, string Uid, short currentLanguageId)
    {
        XsportUser? user = await _db.XsportUsers.Where(u => u.Uid == Uid).FirstOrDefaultAsync() ??
        throw new Exception(UserServiceErrors.user_does_not_exist);

        string img = string.Empty;
        if (dto.File != null) img = await Utils.UploadImageFileAsync(dto.File, user.XsportUserId, webHostEnvironment);
        user.ImagePath = img;
        await _db.SaveChangesAsync();
        await AddFavoriteSportsToUser(Uid, dto.SportsIds);
        return await GetUserProfile(Uid, currentLanguageId);
    }

    public async Task<List<SportDto>> Register(UserRegistrationDto user, string Uid, short currentLanguageId)
    {
        if (user == null) throw new Exception(UserServiceErrors.bad_request_data);
        XsportUser xsportUser = new XsportUser()
        {
            Uid = Uid,
            Email = user.Email,
            Name = user.Name,
            Phone = user.Phone,
            Latitude = user.Latitude,
            Longitude = user.Longitude,
            ImagePath = ""
        };
        await _db.AddAsync<XsportUser>(xsportUser);
        await _db.SaveChangesAsync();

        try
        {
            return _db.Sports.Select(sport => new SportDto()
            {
                SportId = sport.SportId,
                Name = sport.SportTranslations
                .Where(t => t != null)
                .Where(t => t.LanguageId == currentLanguageId)
                .First().Name ?? string.Empty
            }).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task<UserProfileDto> GetUserProfile(string uId, short currentLanguageId)
    {
        XsportUser? user = null;
        try
        {
            user = await _db.XsportUsers.Where(u => u.Uid == uId)
            .Include(u => u.UserSports)
            .ThenInclude(userSport => userSport.Sport)
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
        List<CurrentSportPreference>? currentSportPreferences = userSport?.UserSportPreferences?.Select(userSportPreference => new CurrentSportPreference()
        {
            SportPreferenceId = userSportPreference.SportPreferenceId,
            SportPreferenceName = userSportPreference.SportPreference.SportPreferenceTranslations
            .Where(t => t.LanguageId == currentLanguageId).FirstOrDefault()?.Name ?? string.Empty,
            SportPreferenceValueId = userSportPreference.SportPreferenceValueId,
            SportPreferenceValue = userSportPreference.SportPreferenceValue?.Name ?? string.Empty
        })?.ToList();
        string domainName = httpContextAccessor.HttpContext?.Request.Scheme + "://" + httpContextAccessor.HttpContext?.Request.Host.Value;
        return new UserProfileDto()
        {
            User = new UserInfo()
            {
                UserId = user.XsportUserId,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
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
                CurrentSportId = currentSport?.SportId ?? 0,
                NumOfMatchs = user.UserMatchs?.Count ?? 0,
                Points = sportUserPoints,
                Preferences = currentSportPreferences
            }
        };

    }

    private async Task AddFavoriteSportsToUser(string Uid, List<long> sportsIds)
    {
        var user = await _db.XsportUsers.Include(u => u.UserSports).SingleAsync(u => u.Uid == Uid);
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
}