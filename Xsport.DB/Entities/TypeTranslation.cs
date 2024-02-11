using System.ComponentModel.DataAnnotations;

namespace Xsport.DB.Entities;
public class TypeTranslation
{
    public long TypeTranslationId { get; set; }
    public string? Name { get; set; }
    [Required]
    public long TypeId { get; set; }
    [Required]
    public long LanguageId { get; set; }

    public Language? Language { get; set; }
    public Type? Type { get; set; }
}