using System.ComponentModel.DataAnnotations;

namespace Xsport.DB.Entities;
public class SportPreferenceTranslation
{
    public long SportPreferenceTranslationId { get; set; }
    public string? Name { get; set; }
    [Required]
    public long SportPreferenceId { get; set; }
    [Required]
    public long LanguageId { get; set; }

    public SportPreference? SportPreference { get; set; }
    public Language? Language { get; set; }
}