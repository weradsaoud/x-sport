using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Xsport.Core.StadiumServices;
using Xsport.DTOs.StadiumDtos;
using Xsport.DTOs.UserDtos;

namespace Xsport.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Tags("Stadium")]
    [ApiExplorerSettings(GroupName = "application")]
    public class StadiumController : BaseController
    {
        private IStadiumServices _stadiumService { get; set; }
        public StadiumController(IStadiumServices stadiumService)
        {
            _stadiumService = stadiumService;
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<List<SuggestedStadiumDto>> GetFriendsStadiums(
            [FromQuery] long sportId, [FromQuery] int pageNum, [FromQuery] int pageSize)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not logged in.", 500);
                return await _stadiumService.GetFriendsStadiums(
                    LoggedInUser.Id, sportId, pageNum, pageSize, CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<List<SuggestedStadiumDto>> GetNearByStadiums(
            [FromQuery] long sportId, [FromQuery] int pageNum, [FromQuery] int pageSize)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not logged in.",500);
                return await _stadiumService.GetNearByStadiums(
                    LoggedInUser.Id, sportId, pageNum, pageSize, CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message,500);
            }
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<AboutStadiumDto> GetAboutStadium([FromQuery] long stadiumId)
        {
            try
            {
                return await _stadiumService.GetAboutStadium(stadiumId, CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message,500);
            }
        }
    }
}
