using System.ComponentModel;
using System.Security.Claims;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Swashbuckle.AspNetCore.Annotations;
using Xsport.Common.Emuns;
using Xsport.Common.Enums;
using Xsport.Common.Models;
using Xsport.Core;
using Xsport.DTOs.UserDtos;

namespace Xsport.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Tags("User")]
[ApiExplorerSettings(GroupName = "application")]
public class UserController : BaseController
{
    private IUserServices _userServices;
    public UserController(IUserServices userServices)
    {
        _userServices = userServices;
    }

    [HttpPost]
    public async Task<AuthResult> Register([FromBody] UserRegistrationDto user)
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
            throw new ApiException(
                CurrentLanguageId == (short)LanguagesEnum.English ?
                UserServiceErrors.invalid_inputs_en :
                UserServiceErrors.invalid_inputs_ar, 500);
        }
    }

    [HttpPost]
    public async Task<ResetPassworTokendDto> ForgotPassword([FromBody] ForgotPasswordDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                return await _userServices.ForgotPassword(dto, CurrentLanguageId);
            }
            catch(Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
        else
        {
            throw new ApiException(
                CurrentLanguageId == (short)LanguagesEnum.English ?
                UserServiceErrors.invalid_inputs_en :
                UserServiceErrors.invalid_inputs_ar, 500);
        }
    }

    [HttpPost]
    public async Task<bool> ResetPassword([FromBody] ResetPasswordDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                return await _userServices.ResetPassword(dto, CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
        else
        {
            throw new ApiException(
                CurrentLanguageId == (short)LanguagesEnum.English ?
                UserServiceErrors.invalid_inputs_en :
                UserServiceErrors.invalid_inputs_ar, 500);
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<UserProfileDto> UploadProfilePicture([FromForm] UploadProfilePictureDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException(
                    CurrentLanguageId == (short)LanguagesEnum.English ?
                    UserServiceErrors.not_loggedin_en :
                    UserServiceErrors.not_loggedin_ar);
                return await _userServices.UploadProfilePicture(LoggedInUser.Id, dto, CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
        else
        {
            throw new ApiException(
                CurrentLanguageId == (short)LanguagesEnum.English ?
                UserServiceErrors.invalid_inputs_en :
                UserServiceErrors.invalid_inputs_ar, 500);
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<UserProfileDto> SkipProfilePicture()
    {
        try
        {
            if (LoggedInUser == null) throw new ApiException(
                CurrentLanguageId == (short)LanguagesEnum.English ?
                UserServiceErrors.not_loggedin_en :
                UserServiceErrors.not_loggedin_ar, 500);
            return await _userServices.SkipProfilePicture(LoggedInUser.Id, CurrentLanguageId);
        }
        catch (Exception ex)
        {
            throw new ApiException(ex.Message, 500);
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<short> AccountStatus()
    {
        try
        {
            if (LoggedInUser == null) return (short)RegistrationStatusEnum.unKown;
            return await _userServices.AccountStatus(LoggedInUser);
        }
        catch (Exception ex)
        {
            throw new ApiException(ex.Message, 500);
        }
    }

    //[HttpPost]
    //public async Task<bool> ResendEmailConfirmationCode([FromBody] UserLoginRequest dto)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            return await _userServices.ResendEmailConfirmationCode(dto);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new ApiException(ex.Message, 500);
    //        }
    //    }
    //    else
    //    {
    //        throw new ApiException("Invalid Inputs", 500);
    //    }
    //}

    //[HttpPost]
    //public async Task<ConfirmUserEmailRespDto> ConfirmUserEmail([FromBody] ConfirmEmailDto dto)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            return await _userServices.ConfirmUserEmail(dto, CurrentLanguageId);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new ApiException(ex.Message, 500);
    //        }
    //    }
    //    else
    //    {
    //        throw new ApiException("Invalid Inputs", 500);
    //    }
    //}
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
            throw new ApiException("Invalid Inputs", 500);
        }
    }
    [HttpPost]
    public async Task<LoginResponseDto> GoogleLogin([FromBody] UserGoogleLoginDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                return await _userServices.GoogleLoginAsync(dto, CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
        else
        {
            throw new ApiException("Invalid Inputs", 500);
        }
    }
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[HttpPost]
    //public async Task<UserProfileDto> CompleteRegistration([FromForm] CompleteRegistrationDto dto)
    //{
    //    try
    //    {
    //        if (LoggedInUser == null) throw new ApiException("You are not logged in");
    //        var res = await _userServices.CompleteRegistration(LoggedInUser.Id, dto, CurrentLanguageId);
    //        return res;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new ApiException(ex.Message, 500);
    //    }
    //}
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<UserProfileDto> GetUserProfile()
    {
        try
        {
            if (LoggedInUser == null) throw new ApiException("You are not logged in", 500);
            var userProfile = await _userServices.GetUserProfile(LoggedInUser.Id, CurrentLanguageId);
            return userProfile;
        }
        catch (Exception ex)
        {
            throw new ApiException(ex.Message, 500);
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<UserProfileDto> EditUserProfile([FromForm] EditUserReqDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not logged in", 500);
                return await _userServices.EditUserProfile(LoggedInUser.Id, dto, CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
        else
        {
            throw new ApiException("Invalide Inputs", 500);
        }

    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<bool> ChangePassword([FromBody] ChangePasswordDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not logged in", 500);
                return await _userServices.ChangePassword(LoggedInUser, dto);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
        else
        {
            throw new ApiException("Invalide Inputs", 500);
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<bool> ChangeEmail([FromBody] ChangeEmailDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not logged in", 500);
                return await _userServices.ChangeEmail(LoggedInUser, dto, CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
        else
        {
            throw new ApiException("Invalide Inputs", 500);
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpDelete]
    public async Task<bool> DeleteAccount()
    {
        try
        {
            if (LoggedInUser == null) throw new ApiException("You are not logged in", 500);
            return await _userServices.DeleteAccount(LoggedInUser.Id);
        }
        catch (Exception ex)
        {
            throw new ApiException(ex.Message, 500);
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<UserProfileDto> AddFavoriteSports([FromBody] AddFavoriteSportReqDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not logged in", 500);
                var userProfile = await _userServices.AddFavoriteSports(dto, LoggedInUser.Id, CurrentLanguageId);
                return userProfile;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
        else
        {
            throw new ApiException("Invalide Inputs", 500);
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<UserProfileDto> DeleteFavoriteSports([FromBody] AddFavoriteSportReqDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not logged in", 500);
                var userProfile = await _userServices.DeleteFavoriteSports(dto, LoggedInUser.Id, CurrentLanguageId);
                return userProfile;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
        else
        {
            throw new ApiException("Invalide Inputs", 500);
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<UserProfileDto> EditPreferences([FromBody] EditPreferencesReqDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not logged in", 500);
                var userProfile = await _userServices.EditPreferences(dto, LoggedInUser.Id, CurrentLanguageId);
                return userProfile;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
        else
        {
            throw new ApiException("Invalide Inputs", 500);
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<UserProfileDto> SelectCurrentSport([FromBody] SelectCurrentSportReqDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not logged in", 500);
                return await _userServices.SelectCurrentSport(LoggedInUser.Id, dto.SportId, CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
        else
        {
            throw new ApiException("Invalide inputs", 500);
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<List<PlayersRankingListDto>> GetPlayers([FromQuery] GetPlayersReqDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not logged in.");
                return await _userServices.GetPlayers(LoggedInUser.Id, dto, CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
        else
        {
            throw new ApiException("Invalide inputs.");
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<bool> InrollUserInCourse(InrollUserInCourseDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not logged in.");
                if (dto.UId == null || dto.UId == 0) dto.UId = LoggedInUser.Id;
                return await _userServices.InrollUserInCourse(dto);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
        else
        {
            throw new ApiException("Invalide inputs.");
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<bool> AddAcademyReview(AddAcademyReviewDto dto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not logged in.");
                return await _userServices.AddAcademyReview(LoggedInUser.Id, dto);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
        else
        {
            throw new ApiException("Invalide inputs.");
        }
    }
}