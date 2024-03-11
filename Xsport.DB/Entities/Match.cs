namespace Xsport.DB.Entities;
public class Match
{
    public long MatchId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public long SportId { get; set; }

    public Sport? Sport { get; set; }
    public ICollection<UserMatch> UserMatchs { get; set; } = null!;
}