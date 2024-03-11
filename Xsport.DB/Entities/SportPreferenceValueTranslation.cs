using System.ComponentModel.DataAnnotations;

namespace Xsport.DB.Entities;
public class SportPreferenceValueTranslation
{
    public long SportPreferenceValueTranslationId { get; set; }
    public string Name { get; set; }
    [Required]
    public long SportPreferenceValueId { get; set; }
    [Required]
    public long LanguageId { get; set; }

    public SportPreferenceValue? SportPreferenceValue { get; set; }
    public Language? Language { get; set; }
}