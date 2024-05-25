using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Xsport.Common.Configurations;
using Xsport.Common.Constants;
using Xsport.Common.Emuns;
using Xsport.Core.EmailServices;
using Xsport.Core.EmailServices.Models;
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
        public DashboardUserServices(
            UserManager<XsportUser> userManager,
            IUserServices userService,
            IEmailService emailservice,
            IOptionsMonitor<GeneralConfig> _optionsMonitor)
        {
            _userManager = userManager;
            _userService = userService;
            _emailService = emailservice;
            GeneralConfig = _optionsMonitor.CurrentValue;
        }
        public async Task<bool> Register(UserRegistrationDto user)
        {
            try
            {
                if (user == null) throw new Exception(UserServiceErrors.bad_request_data_en);
                if (user.Email.IsNullOrEmpty()) throw new Exception("Email is required");
                //check if user exists
                var existingUser = await _userManager.FindByEmailAsync(user.Email);
                if (existingUser != null) throw new Exception($"{user.Email} is already taken");
                //string confirmationCode = await SendActivationCode(user.Email);
                //if (confirmationCode.IsNullOrEmpty())
                //    throw new Exception("Could not generate confirmation code");
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
                //AuthResult jwtToken = await _userService.GenerateJwtToken(xsportUser, GeneralConfig?.EnableTwoFactor ?? false);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
