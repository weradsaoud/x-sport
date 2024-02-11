namespace Xsport.DB.Entities;

public class Type
{
    public long TypeId { get; set; }
    public string? Name { get; set; }

    public ICollection<UserType>? UserTypes { get; set; }
    public ICollection<TypeTranslation>? TypeTranslations { get; set; }
}
