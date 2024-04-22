using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
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
        private IDashboardUserServices _userService { get; set; }
        public DashboardUserController(IDashboardUserServices userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<bool> Register([FromBody] UserRegistrationDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await _userService.Register(dto);
                }
                catch (Exception ex)
                {
                    throw new ApiException(ex.Message, 500);
                }
            }
            else
            {
                throw new ApiException("Invalid Input.", 500);
            }
        }
    }
}
