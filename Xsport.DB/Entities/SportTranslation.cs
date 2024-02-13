using System.ComponentModel.DataAnnotations;

namespace Xsport.DB.Entities;
public class SportTranslation
{
    public long SportTranslationId { get; set; }
    public string? Name { get; set; }
    [Required]
    public long SportId { get; set; }
    [Required]
    public long LanguageId { get; set; }

    public Sport Sport { get; set; } = null!;
    public Language Language { get; set; } =null!;
}