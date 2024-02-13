using System.ComponentModel.DataAnnotations;

namespace Xsport.DB.Entities;
public class UserMatch
{
    public long UserMatchId { get; set; }
    public bool IsOrganizer { get; set; }
    [Required]
    public long XsportUserId { get; set; }
    [Required]
    public long MatchId { get; set; }

    public XsportUser User { get; set; } = null!;
    public Match Match { get; set; } = null!;
}