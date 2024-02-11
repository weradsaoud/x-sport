namespace Xsport.DB.Entities;
public class Language
{
    public long LanguageId { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }

    public ICollection<SportTranslation>? SportTranslations { get; set; }
    public ICollection<TypeTranslation>? TypeTranslations { get; set; }
    public ICollection<SportPreferenceTranslation>? SportPreferenceTranslations { get; set; }
    public ICollection<SportPreferenceValueTranslation>? SportPreferenceValueTranslations { get; set; }
}