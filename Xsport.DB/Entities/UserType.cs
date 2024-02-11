using System.ComponentModel.DataAnnotations;
using Xsport.DB.Entities;

namespace Xsport.DB.Entities;
public class UserType
{
    public long UserTypeId { get; set; }
    public long XsportUserId { get; set; }
    public long TypeId { get; set; }
    public long SportId { get; set; }

    [Required]
    public XsportUser? XsportUser { get; set; }
    [Required]
    public Type? Type { get; set; }
    [Required]
    public Sport? Sport { get; set; }
}