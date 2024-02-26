using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Xsport.DB.Entities;

namespace Xsport.DB.Entities;
public class UserRole:IdentityUserRole<long>
{
    //public long UserTypeId { get; set; }
    //public long XsportUserId { get; set; }
    //public long XsportRoleId { get; set; }
    public long SportId { get; set; }

    //[Required]
    //public XsportUser? XsportUser { get; set; }
    //[Required]
    //public XsportRole? XsportRole { get; set; }
    [Required]
    public Sport? Sport { get; set; }
}