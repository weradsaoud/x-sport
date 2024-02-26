namespace Xsport.DTOs.UserDtos
{
    public class UserProfileDto
    {
        public UserInfo? User { get; set; }
        public List<FavoriteSport>? FavoriteSports { get; set; }
        public CurrentSport CurrentSport { get; set; } = null!;
    }

    public class UserInfo
    {
        public long UserId { get; set; }
        public int LoyaltyPoints { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public string? ImgURL { get; set; }
    }
    public class FavoriteSport
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsCurrentState { get; set; }
    }
    public class CurrentSport
    {
        public long CurrentSportId { get; set; }
        public int Points { get; set; }
        public int LevelPercent { get; set; }
        public string UserLevel{ get; set; }=null!;
        public List<CurrentSportPreference>? Preferences { get; set; }
        public int NumOfMatchs { get; set; }
    }
    public class CurrentSportPreference
    {
        public long SportPreferenceId { get; set; }
        public string SportPreferenceName { get; set; } = null!;
        public List<SportPreferenceValue> SportPreferenceValues { get; set; } = null!;
        public long SportPreferenceValueId { get; set; }
    }
    public class SportPreferenceValue
    {
        public long SportPreferenceValueId { get; set; }
        public string SportPreferenceValueName { get;set; } = null!;
}