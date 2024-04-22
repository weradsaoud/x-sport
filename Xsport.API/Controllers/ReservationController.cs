using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Xsport.Core.ReservationServices;
using Xsport.DTOs.ReservationDtos;
using Xsport.DTOs.StadiumDtos;

namespace Xsport.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Tags("Reservation")]
    [ApiExplorerSettings(GroupName = "application")]
    public class ReservationController : BaseController
    {
        private IReservationSrvice _reservationSrvice { get; set; }
        public ReservationController(IReservationSrvice reservationSrvice)
        {
            _reservationSrvice = reservationSrvice;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<List<SuggestedStadiumDto>> GetSportStadiums([FromQuery] Criteria criteria)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    return await _reservationSrvice.GetSportStadiums(criteria, CurrentLanguageId);
                }
                catch (Exception ex)
                {
                    throw new ApiException(ex.Message, 500);
                }
            }
            else
            {
                throw new ApiException("Invalid Input", 500);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<List<ReservedDay>> GetReservedTimes([FromQuery] long stadiumFloorId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await _reservationSrvice.GetReservedTimes(stadiumFloorId, CurrentLanguageId);
                }
                catch (Exception ex)
                {
                    throw new ApiException(ex.Message, 500);
                }
            }
            else
            {
                throw new ApiException("Invalid Input", 500);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<ReservationDetails> Reserve([FromBody] ReserveDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (LoggedInUser == null) throw new ApiException("You are not logged in.");
                    return await _reservationSrvice.Reserve(dto, LoggedInUser.Id,CurrentLanguageId);
                }
                catch (Exception ex)
                {
                    throw new ApiException(ex.Message, 500);
                }
            }
            else
            {
                throw new ApiException("Invalid Input", 500);
            }
        }
    }
}
