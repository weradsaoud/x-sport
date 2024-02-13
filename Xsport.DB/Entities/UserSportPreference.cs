using System.ComponentModel.DataAnnotations;

namespace Xsport.DB.Entities;
public class UserSportPreference
{
    public long UserSportPreferenceId { get; set; }
    [Required]
    public long UserSportId { get; set; }
    [Required]
    public long SportPreferenceId { get; set; }
    [Required]
    public long SportPreferenceValueId { get; set; }

    public UserSport? UserSport { get; set; }
    public SportPreference SportPreference { get; set; } = null!;
    public SportPreferenceValue? SportPreferenceValue { get; set; }
}