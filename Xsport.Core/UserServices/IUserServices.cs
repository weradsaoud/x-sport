
using Xsport.DTOs.UserDtos;

namespace Xsport.Core;
public interface IUserServices
{
    public UserProfileDto Register(UserRegistrationDto user);
}