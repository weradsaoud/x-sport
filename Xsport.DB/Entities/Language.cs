namespace Xsport.DB.Entities;
public class Language
{
    public long LanguageId { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }

    public ICollection<SportTranslation>? SportTranslations { get; set; }
    public ICollection<XsportRoleTranslation>? XsportRoleTranslations { get; set; }
    public ICollection<SportPreferenceTranslation>? SportPreferenceTranslations { get; set; }
    public ICollection<SportPreferenceValueTranslation>? SportPreferenceValueTranslations { get; set; }
    public ICollection<LevelTranslation> LevelTranslations { get; set; } = null!;
    public ICollection<AcademyTranslation> AcademyTranslations { get; set; } = null!;
    public ICollection<AgeCategoryTranslation> AgeCategoryTranslations { get; set; } = null!;
    public ICollection<WorkingDayTranslation> WorkingDayTranslations { get; set; } = null!;
    public ICollection<RelativeTranslation> RelativeTranslations { get; set; } = null!;
    public ICollection<ServiceTranslation> ServiceTranslations { get; set; } = null!;
    public ICollection<ServiceValueTranslation> ServiceValueTranslations { get; set; } = null!;
    public ICollection<CourseTranslation> CourseTranslations { get; set; } = null!;
}