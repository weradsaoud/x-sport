namespace Xsport.DB.Entities;
public class SportPreference
{
    public long SportPreferenceId { get; set; }
    public long SportId { get; set; }

    public Sport? Sport { get; set; }
    public ICollection<SportPreferenceTranslation> SportPreferenceTranslations { get; set; } = null!;
    public ICollection<SportPreferenceValue>? SportPreferenceValues { get; set; }
    public ICollection<UserSportPreference>? UserSportPreferences { get; set; }
}