using System.Security.Claims;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xsport.Core;
using Xsport.DTOs.UserDtos;

namespace Xsport.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : BaseController
{
    private IUserServices _userServices;
    public UserController(IUserServices userServices)
    {
        _userServices = userServices;
    }

    [HttpPost]
    public async Task<List<SportDto>> Register([FromBody] UserRegistrationDto user)
    {
        try
        {
            var sports = await _userServices.Register(user, Uid, CurrentLanguageId);
            return sports;
        }
        catch (Exception ex)
        {
            throw new ApiException(ex.Message, 500);
        }
    }
    [HttpPost]
    public async Task<UserProfileDto> CompleteRegistration([FromForm] CompleteRegistrationDto dto)
    {
        try
        {
            var userProfile = await _userServices.CompleteRegistration(dto, Uid, CurrentLanguageId);
            return userProfile;
        }
        catch (Exception ex)
        {
            throw new ApiException(ex.Message, 500);
        }
    }

}