using System.ComponentModel.DataAnnotations;

namespace Xsport.DB.Entities;
public class XsportRoleTranslation
{
    public long XsportRoleTranslationId { get; set; }
    public string? Name { get; set; }
    [Required]
    public long XsportRoleId { get; set; }
    [Required]
    public long LanguageId { get; set; }

    public Language? Language { get; set; }
    public XsportRole? XsportRole { get; set; }
}