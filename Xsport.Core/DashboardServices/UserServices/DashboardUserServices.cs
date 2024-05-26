using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Xsport.Common.Configurations;
using Xsport.Common.Constants;
using Xsport.Common.Emuns;
using Xsport.Common.Enums;
using Xsport.Common.Models;
using Xsport.Common.Utils;
using Xsport.Core.EmailServices;
using Xsport.Core.EmailServices.Models;
using Xsport.Core.LoggerServices;
using Xsport.DB;
using Xsport.DB.Entities;
using Xsport.DTOs.UserDtos;

namespace Xsport.Core.DashboardServices.UserServices
{
    public class DashboardUserServices : IDashboardUserServices
    {
        private UserManager<XsportUser> _userManager { get; }
        private IUserServices _userService { get; set; }
        private IEmailService _emailService { get; set; }
        GeneralConfig GeneralConfig { get; }
        private readonly ILoggerManager _logger;
        private readonly AppDbContext _db;
        public DashboardUserServices(
            UserManager<XsportUser> userManager,
            IUserServices userService,
            IEmailService emailservice,
            IOptionsMonitor<GeneralConfig> _optionsMonitor,
            ILoggerManager logger,
            AppDbContext db)
        {
            _userManager = userManager;
            _userService = userService;
            _emailService = emailservice;
            GeneralConfig = _optionsMonitor.CurrentValue;
            _logger = logger;
            _db = db;
        }
        public async Task<AuthResult> Register(UserRegistrationDto user, short currentLanguageId)
        {
            try
            {
                if (user == null) throw new Exception(
                    currentLanguageId == (short)LanguagesEnum.English ?
                    UserServiceErrors.bad_request_data_en :
                    UserServiceErrors.bad_request_data_ar);
                if (user.Email.IsNullOrEmpty()) throw new Exception(
                    currentLanguageId == (short)LanguagesEnum.English ?
                    UserServiceErrors.required_email_en :
                    UserServiceErrors.required_email_ar);
                //check if user exists
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser != null) throw new Exception(user.Email +
                    ((currentLanguageId == (short)LanguagesEnum.English) ?
                    UserServiceErrors.email_taken_en :
                    UserServiceErrors.email_taken_ar));
                string confirmationCode = _userService.GenerateEmailConfirmationCode();
                XsportUser xsportUser = new XsportUser()
                {
                    Email = user.Email,
                    UserName = user.Email,
                    XsportName = user.Name,
                    PhoneNumber = user.Phone,
                    LoyaltyPoints = 0,
                    ImagePath = "",
                    AuthenticationProvider = "EmailPassword"
                };
                var isCreated = await _userManager.CreateAsync(xsportUser, user.Password);
                if (!isCreated.Succeeded)
                {
                    string errorMessage = string.Empty;
                    foreach (var item in isCreated.Errors)
                    {
                        errorMessage = "__" + errorMessage + item.Description + "\n";
                    }

                    throw new Exception(errorMessage);
                }
                await _userManager.AddToRoleAsync(xsportUser, XsportRoles.PropertyOwner);
                try
                {
                    var message = new Message(
                    new List<string>() { user.Email ?? string.Empty },
                        "Account Confirmation", confirmationCode, null);
                    await _emailService.SendEmailAsync(message);
                }
                catch (Exception)
                {
                    throw new Exception("Confirmation email could not be sent. " +
                        "Please, make sure you entered a valide email address");
                }
                AuthResult jwtToken = await _userService.GenerateJwtToken(xsportUser, GeneralConfig?.EnableTwoFactor ?? false);
                return jwtToken;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Task<short> AccountStatus(XsportUser user)
        {
            try
            {
                return Task.FromResult(user.RegistrationStatus);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResetPassworTokendDto> ForgotPassword(ForgotPasswordDto dto, short currentLanguageId)
        {
            try
            {
                return await _userService.ForgotPassword(dto, currentLanguageId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ResetPassword(ResetPasswordDto dto, short currentLanguageId)
        {
            try
            {
                return await _userService.ResetPassword(dto, currentLanguageId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<LoginResponseDto> LoginAsync(UserLoginRequest user, short currentLanguageId)
        {
            try
            {
                _logger.LogInfo($"User with email = ${user.Email} trying to login.");
                var existingUser = await _userManager.FindByEmailAsync(user.Email) ??
                    throw new Exception(currentLanguageId == (short)LanguagesEnum.English ?
                    UserServiceErrors.user_does_not_exist_en :
                    UserServiceErrors.user_does_not_exist_ar);

                var userRole = await _userManager.GetRolesAsync(existingUser);
                if (!userRole.Contains(XsportRoles.PropertyOwner))
                    throw new Exception(currentLanguageId == (short)LanguagesEnum.English ?
                    UserServiceErrors.user_does_not_exist_en :
                    UserServiceErrors.user_does_not_exist_ar);

                var isLockout = await _userManager.IsLockedOutAsync(existingUser);
                if (isLockout)
                    throw new Exception("User is locked out");
                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);
                if (!isCorrect)
                {
                    // increase the failed log in count
                    if (_userManager.SupportsUserLockout && _userManager.GetLockoutEnabledAsync(existingUser).Result)
                    {
                        await _userManager.AccessFailedAsync(existingUser);
                    }
                    throw new Exception("Invalid authentication");
                }
                //if (GeneralConfig?.EnableTwoFactor ?? false)
                //{
                //    if (await userManager.GetTwoFactorEnabledAsync(existingUser))
                //        return await GenerateOTPFor2StepVerification(existingUser, ai);
                //}

                var jwtToken = await _userService.GenerateJwtToken(existingUser, GeneralConfig?.EnableTwoFactor ?? false);

                // if log in success then reset the failed log in count to zero
                if (_userManager.SupportsUserLockout && _userManager.GetAccessFailedCountAsync(existingUser).Result > 0)
                {
                    await _userManager.ResetAccessFailedCountAsync(existingUser);
                }
                var userProfile = await GetUserProfile(existingUser.Id, currentLanguageId);
                return new LoginResponseDto
                {
                    AuthResult = jwtToken,
                    UserProfile = userProfile,
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<LoginResponseDto> GoogleLoginAsync(UserGoogleLoginDto dto, short currentLanguageId)
        {
            string Uid;
            try
            {
                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(dto.FirebaseToken);
                Uid = decodedToken.Uid;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            try
            {
                if (dto.Email.IsNullOrEmpty()) throw new Exception(
                    currentLanguageId == (short)LanguagesEnum.English ?
                    UserServiceErrors.required_email_en :
                    UserServiceErrors.required_email_ar);
                var existingUser = await _db.XsportUsers.SingleOrDefaultAsync(u => u.Uid == Uid);
                if (existingUser == null)
                {
                    existingUser = await _userManager.FindByEmailAsync(dto.Email);
                    if (existingUser == null)
                    {
                        XsportUser xsportUser = new XsportUser()
                        {
                            Uid = Uid,
                            Email = dto.Email,
                            EmailConfirmed = true,
                            UserName = dto.Email,
                            XsportName = dto.Name,
                            PhoneNumber = dto.Phone,
                            LoyaltyPoints = 0,
                            ImagePath = "",
                            AuthenticationProvider = "Google",
                            RegistrationStatus = (short)RegistrationStatusEnum.registredWithGoogle,
                        };
                        var isCreated = await _userManager.CreateAsync(xsportUser);
                        if (!isCreated.Succeeded) throw new Exception("User could not be created successfully.");
                        await _userManager.AddToRoleAsync(xsportUser, XsportRoles.PropertyOwner);
                        var jwtToken = await _userService.GenerateJwtToken(xsportUser, GeneralConfig?.EnableTwoFactor ?? false);
                        return new LoginResponseDto
                        {
                            AuthResult = jwtToken,
                            UserProfile = null
                        };
                    }
                    else
                    {
                        var userRole = await _userManager.GetRolesAsync(existingUser);
                        if (!userRole.Contains(XsportRoles.PropertyOwner))
                            throw new Exception(currentLanguageId == (short)LanguagesEnum.English ?
                            UserServiceErrors.user_does_not_exist_en :
                            UserServiceErrors.user_does_not_exist_ar);

                        var jwtToken = await _userService.GenerateJwtToken(existingUser, GeneralConfig?.EnableTwoFactor ?? false);
                        var userProfile = await GetUserProfile(existingUser.Id, currentLanguageId);
                        return new LoginResponseDto
                        {
                            AuthResult = jwtToken,
                            UserProfile = userProfile
                        };
                    }

                }
                else
                {
                    var userRole = await _userManager.GetRolesAsync(existingUser);
                    if (userRole.Contains(XsportRoles.PropertyOwner))
                        throw new Exception(currentLanguageId == (short)LanguagesEnum.English ?
                        UserServiceErrors.user_does_not_exist_en :
                        UserServiceErrors.user_does_not_exist_ar);

                    var jwtToken = await _userService.GenerateJwtToken(existingUser, GeneralConfig?.EnableTwoFactor ?? false);
                    var userProfile = await GetUserProfile(existingUser.Id, currentLanguageId);
                    return new LoginResponseDto
                    {
                        AuthResult = jwtToken,
                        UserProfile = userProfile
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<UserProfileDto> UploadProfilePicture(long uId, UploadProfilePictureDto dto, short currentLanguageId)
        {
            try
            {
                return await _userService.UploadProfilePicture(uId, dto, currentLanguageId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<UserProfileDto> SkipProfilePicture(long uId, short currentLanguageId)
        {
            try
            {
                return await _userService.SkipProfilePicture(uId, currentLanguageId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserProfileDto> CompleteRegistration(long uId, CompleteRegistrationDto dto, short currentLanguageId)
        {
            try
            {
                return await _userService.CompleteRegistration(uId, dto, currentLanguageId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<UserProfileDto> GetUserProfile(long uId, short currentLanguageId)
        {
            try
            {
                return await _userService.GetUserProfile(uId, currentLanguageId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<UserProfileDto> EditUserProfile(long uId, EditUserReqDto dto, short currentLanguageId)
        {
            try
            {
                return await _userService.EditUserProfile(uId, dto, currentLanguageId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ChangePassword(XsportUser user, ChangePasswordDto dto)
        {
            try
            {
                return await _userService.ChangePassword(user, dto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> ChangeEmail(XsportUser user, ChangeEmailDto dto, short currentLanguageId)
        {
            try
            {
                return await _userService.ChangeEmail(user, dto, currentLanguageId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteAccount(long uId)
        {
            try
            {
                return await _userService.DeleteAccount(uId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
