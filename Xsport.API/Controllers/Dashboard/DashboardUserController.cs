using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xsport.Common.Emuns;
using Xsport.Common.Enums;
using Xsport.Common.Models;
using Xsport.Core;
using Xsport.Core.DashboardServices.UserServices;
using Xsport.DTOs.UserDtos;

namespace Xsport.API.Controllers.Dashboard
{
    [ApiController]
    [Route("api/dashboard/[controller]/[action]")]
    [Tags("DashboardUser")]
    [ApiExplorerSettings(GroupName = "dashboard")]
    public class DashboardUserController : BaseController
    {
        private IDashboardUserServices _dashboardUserServices;
        public DashboardUserController(IDashboardUserServices dashboardUserServices)
        {
            _dashboardUserServices = dashboardUserServices;
        }
        [HttpPost]
        public async Task<AuthResult> Register([FromBody] UserRegistrationDto user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await _dashboardUserServices.Register(user, CurrentLanguageId);
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
        public async Task<ResetPassworTokendDto> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await _dashboardUserServices.ForgotPassword(dto, CurrentLanguageId);
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
        public async Task<bool> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await _dashboardUserServices.ResetPassword(dto, CurrentLanguageId);
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
                    return await _dashboardUserServices.UploadProfilePicture(LoggedInUser.Id, dto, CurrentLanguageId);
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
                return await _dashboardUserServices.SkipProfilePicture(LoggedInUser.Id, CurrentLanguageId);
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
                return await _dashboardUserServices.AccountStatus(LoggedInUser);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }

        [HttpPost]
        public async Task<LoginResponseDto> Login([FromBody] UserLoginRequest userLoginRequest)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await _dashboardUserServices.LoginAsync(userLoginRequest, CurrentLanguageId);
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
                    return await _dashboardUserServices.GoogleLoginAsync(dto, CurrentLanguageId);
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<UserProfileDto> GetUserProfile()
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not logged in", 500);
                var userProfile = await _dashboardUserServices.GetUserProfile(LoggedInUser.Id, CurrentLanguageId);
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
                    return await _dashboardUserServices.EditUserProfile(LoggedInUser.Id, dto, CurrentLanguageId);
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
    }
}
