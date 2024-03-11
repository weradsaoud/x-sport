using System.ComponentModel.DataAnnotations;

namespace Xsport.DB.Entities;
public class UserSportPreferenceValue
{
    public long UserSportPreferenceValueId { get; set; }
    [Required]
    public long UserSportId { get; set; }
    [Required]
    public long SportPreferenceValueId { get; set; }

    public UserSport? UserSport { get; set; }
    public SportPreferenceValue? SportPreferenceValue { get; set; }
}