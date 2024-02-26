namespace Xsport.DB.Entities;
public class Sport
{
    public long SportId { get; set; }
    public string? Name { get; set; }
    public int NumOfTeams { get; set; }
    public int NumOfPlayers { get; set; }
    public int NumOfReferees { get; set; }
    public int NumOfRounds { get; set; }
    public int RoundPeriod { get; set; }
    public int NumOfBreaks { get; set; }
    public int BreakPeriod { get; set; }
    public bool HasExtraRounds { get; set; }
    public int NumOfExtraRounds { get; set; }

    public ICollection<UserRole>? UserRoles { get; set; }
    public ICollection<SportTranslation> SportTranslations { get; set; } = null!;
    public ICollection<Match>? Matches { get; set; }
    public ICollection<SportPreference> SportPreferences { get; set; } = null!;
    public ICollection<Level> Levels { get; set; } = null!;
}