using Xsport.Db;
using Xsport.DB.Entities;
using Xsport.DTOs.UserDtos;

namespace Xsport.Core;
public class UserServices : IUserServices
{
    private AppDbContext _db;
    public UserServices(AppDbContext db)
    {
        _db = db;
    }
    public UserProfileDto Register(UserRegistrationDto user)
    {
        XsportUser xsportUser = new XsportUser()
        {
            Uid = user.Uid,
            Email = user.Email,
            Name = user.Name,
            Phone = user.Phone,
            Latitude = user.Latitude,
            Longitude = user.Longitude,
            ImagePath = ""
        };
        return new UserProfileDto();
    }
}