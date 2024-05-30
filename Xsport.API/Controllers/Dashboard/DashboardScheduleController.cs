using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Xsport.Common.Emuns;
using Xsport.Common.Enums;
using Xsport.Common.Models;
using Xsport.Core;
using Xsport.Core.DashboardServices.UserServices;
using Xsport.Core.ReservationServices;
using Xsport.DB.Entities;
using Xsport.DTOs.ReservationDtos;
using Xsport.DTOs.StadiumDtos.DashboardDtos;
using Xsport.DTOs.UserDtos;

namespace Xsport.API.Controllers.Dashboard
{
    [ApiController]
    [Route("api/dashboard/[controller]/[action]")]
    [Tags("DashboardSchedule")]
    [ApiExplorerSettings(GroupName = "dashboard")]
    public class DashboardScheduleController : BaseController
    {
        private IDashboarScheduleServices _dashboarScheduleServices;
        public DashboardScheduleController(IDashboarScheduleServices dashboarScheduleServices)
        {
            _dashboarScheduleServices = dashboarScheduleServices;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<List<DashboardDailyReservationSlotDto>> GetStadiumDailyReservationsSlots([FromBody] DailyReservationDto dto)
        {
                try
                {
                if (LoggedInUser == null) throw new ApiException("You are not signed in.");
                return await _dashboarScheduleServices.GetStadiumFloorDailyReservationsSlots(dto, CurrentLanguageId);
                }
                catch (Exception ex)
                {
                    throw new ApiException(ex.Message, 500);
                }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<List<DashboardDailyReservationDto>> GetStadiumDailyReservations([FromBody] DailyReservationDto dto)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not signed in.");
                return await _dashboarScheduleServices.GetStadiumFloorDailyReservations(dto, CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<List<DashboardMonthlyReservationDto>> GetStadiumMonthlyReservations([FromBody] MonthlyReservationDto dto)
        {
                try
                {
                if (LoggedInUser == null) throw new ApiException("You are not signed in.");
                return await _dashboarScheduleServices.GetStadiumMonthlyReservations(dto, CurrentLanguageId);
                }
                catch (Exception ex)
                {
                    throw new ApiException(ex.Message, 500);
                }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<OwnerStadiumDto> GetOwnerStadiums()
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not signed in.");
                return await _dashboarScheduleServices.GetOwnerStadiums(LoggedInUser.Id);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<StadiumFloorsDto> GetStadiumFloors(long stadiumId)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not signed in.");
                return await _dashboarScheduleServices.GetStadiumFloors(stadiumId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }

    }
}
