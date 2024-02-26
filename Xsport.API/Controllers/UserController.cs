using System.Security.Claims;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public async Task<bool> Register([FromBody] UserRegistrationDto user)
    {
        if (ModelState.IsValid)
        {
            try
            {
                return await _userServices.Register(user, CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
        else
        {
            throw new ApiException("Invalid Inputs");
        }
    }
    [HttpGet]
    public async Task<bool> ConfirmUserEmail([FromQuery] ConfirmEmailDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                return await _userServices.ConfirmUserEmail(dto.UserId, dto.Token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        else
        {
            throw new ApiException("Invalid Inputs", 500);
        }
    }
    [HttpPost]
    public async Task<LoginResponseDto> Login([FromBody] UserLoginRequest userLoginRequest)
    {
        if (ModelState.IsValid)
        {
            try
            {
                return await _userServices.LoginAsync(userLoginRequest, CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
        else
        {
            throw new ApiException("Invalid Inputs");
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<UserProfileDto> CompleteRegistration([FromForm] CompleteRegistrationDto dto)
    {
        try
        {
            if (LoggedInUser == null) throw new ApiException("You are not logged in");
            var userProfile = await _userServices.CompleteRegistration(dto, LoggedInUser.Id, CurrentLanguageId);
            return userProfile;
        }
        catch (Exception ex)
        {
            throw new ApiException(ex.Message, 500);
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<UserProfileDto> GetUserProfile()
    {
        try
        {
            if (LoggedInUser == null) throw new Exception("You are not logged in");
            var userProfile = await _userServices.GetUserProfile(LoggedInUser.Id, CurrentLanguageId);
            return userProfile;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}