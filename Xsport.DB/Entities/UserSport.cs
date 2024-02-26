using System.ComponentModel.DataAnnotations;

namespace Xsport.DB.Entities;
public class UserSport
{
    public long UserSportId { get; set; }
    public int Points { get; set; }
    public bool IsCurrentState { get; set; }
    [Required]
    public long XsportUserId { get; set; }
    [Required]
    public long SportId { get; set; }

    public XsportUser? XsportUser { get; set; }
    public Sport Sport { get; set; } = null!;
    public ICollection<UserSportPreference> UserSportPreferences { get; set; } = null!;
}