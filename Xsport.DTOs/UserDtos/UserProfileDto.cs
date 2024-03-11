namespace Xsport.DTOs.UserDtos
{
    public class UserProfileDto
    {
        public UserInfoDto? User { get; set; }
        public List<FavoriteSportDto>? FavoriteSports { get; set; }
        public CurrentSportDto CurrentSport { get; set; } = null!;
        public int Followers { get; set; }
        public int Following { get; set; }
    }

    public class UserInfoDto
    {
        public long UserId { get; set; }
        public int LoyaltyPoints { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public string? ImgURL { get; set; }
    }
    public class FavoriteSportDto
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsCurrentState { get; set; }
        public List<SportPreferenceDto>? Preferences { get; set; }

    }
    public class CurrentSportDto
    {
        public long CurrentSportId { get; set; }
        public int Points { get; set; }
        public int LevelPercent { get; set; }
        public string UserLevel{ get; set; }=null!;
        public int NumOfMatchs { get; set; }
        public List<SportLevel> Levels { get; set; } = null!;
    }
    public class SportLevel
    {
        public string LevelName { get; set; } = null!;
        public int LevelMaxPoints { get; set; }
    }
    public class SportPreferenceDto
    {
        public long SportPreferenceId { get; set; }
        public string SportPreferenceName { get; set; } = null!;
        public List<SportPreferenceValueDto> SportPreferenceValues { get; set; } = null!;
        public long SelectedPreferenceValueId { get; set; }
    }
    public class SportPreferenceValueDto
    {
        public long SportPreferenceValueId { get; set; }
        public string SportPreferenceValueName { get; set; } = null!;
    }
}