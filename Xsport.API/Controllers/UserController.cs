using Microsoft.AspNetCore.Mvc;
using Xsport.Core;
using Xsport.DTOs.UserDtos;

namespace Xsport.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : ControllerBase
{
    private UserServices _userServices;
    public UserController(UserServices userServices)
    {
        _userServices = userServices;
    }

    [HttpPost]
    public UserProfileDto Register([FromBody] UserRegistrationDto user)
    {
        var userProfile = _userServices.Register(user);
        return userProfile;
    }
}