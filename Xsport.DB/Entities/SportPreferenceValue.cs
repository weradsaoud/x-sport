using System.ComponentModel.DataAnnotations;

namespace Xsport.DB.Entities;
public class SportPreferenceValue
{
    public long SportPreferenceValueId { get; set; }
    public string Name { get; set; } = null!;
    [Required]
    public long SportPreferenceId { get; set; }

    public SportPreference? SportPreference { get; set; }
    public ICollection<SportPreferenceValueTranslation>? SportPreferenceValueTranslations { get; set; }
    public UserSportPreference? UserSportPreference { get; set; }
}