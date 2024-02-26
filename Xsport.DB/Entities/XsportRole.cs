using Microsoft.AspNetCore.Identity;

namespace Xsport.DB.Entities;

public class XsportRole: IdentityRole<long>
{
    //public long TypeId { get; set; }
    //public string? Name { get; set; }

    //public ICollection<UserRole>? UserRoles { get; set; }
    public ICollection<XsportRoleTranslation>? XsportRoleTranslations { get; set; }
}
