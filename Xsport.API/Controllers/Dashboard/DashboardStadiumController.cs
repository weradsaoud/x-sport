using AutoWrapper.Wrappers;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Xsport.Common.Enums;
using Xsport.Common.Utils;
using Xsport.Core.DashboardServices.StadiumServices;
using Xsport.DB.Entities;
using Xsport.DTOs.StadiumDtos.DashboardDtos;
using Xsport.DTOs.StadiumDtos.MNGDtos;

namespace Xsport.API.Controllers.Dashboard
{
    [ApiController]
    [Route("api/dashboard/[controller]/[action]")]
    [Tags("DashboardStadium")]
    [ApiExplorerSettings(GroupName = "dashboard")]
    public class DashboardStadiumController : BaseController
    {
        private readonly IDashboardStadiumServices _stadiumDashboardService;

        public DashboardStadiumController(IDashboardStadiumServices stadiumDashboardService)
        {
            _stadiumDashboardService = stadiumDashboardService;
        }

        // Start a new stadium creation process or retrieve an existing one
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<List<StadiumProcessCreationDto>> GetStadiumCreatopnProcesses()
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not signed in.");
                return await _stadiumDashboardService.GetStadiumCreatopnProcesses(LoggedInUser.Id);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[HttpPost]
        //public async Task<bool> CompleteCreationProcess(long processId)
        //{
        //    try
        //    {
        //        if (LoggedInUser == null) throw new ApiException("You are not signed in.");
        //        return await _stadiumDashboardService.CompleteCreationProcess(LoggedInUser.Id, processId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiException(ex.Message);
        //    }
        //}

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<bool> AddStadiumDecription(DashboardStadiumDiscriptionDto dto, long processId)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not signed in.");
                return await _stadiumDashboardService.AddStadiumDecription(dto, LoggedInUser.Id, processId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<bool> AddStadiumLocation(DashboardStadiumLocationDto dto, long processId)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not signed in.");
                return await _stadiumDashboardService.AddStadiumLocation(dto, LoggedInUser.Id, processId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<bool> AddStadiumFloors(DashboardStadiumFloorsDto dto, long processId)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not signed in.");
                return await _stadiumDashboardService.AddStadiumFloors(dto, LoggedInUser.Id, processId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<bool> AddStadiumServices(DashboardStadiumServicesDto dto, long processId)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not signed in.");
                return await _stadiumDashboardService.AddStadiumServices(dto, LoggedInUser.Id, processId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<bool> AddStadiumName(DashboardStadiumNameDto dto, long processId)
        {

            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not signed in.");
                return await _stadiumDashboardService.AddStadiumName(dto, LoggedInUser.Id, processId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<bool> AddStadiumMultimedia(DashboardStadiumMultimediaDto dto, long processId)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not signed in.");
                return await _stadiumDashboardService.AddStadiumMultimedia(dto, LoggedInUser.Id, processId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<bool> AddReservationType(long reservationType, long processId)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not signed in.");
                // TODO : Mousa to add reservation type into database and implement the function in the stadiumDashboardService
                return await _stadiumDashboardService.AddReservationType(reservationType, LoggedInUser.Id, processId);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<bool> AddStadiumFloorsPrices(List<DashboardStadiumFloorPriceDto> floors, long processId)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not signed in.");
                return await _stadiumDashboardService.AddStadiumFloorsPrices(floors, LoggedInUser.Id, processId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<bool> AddStadiumWorkingDays([FromBody] DashboardStadiumWorkingDaysDto dto, long processId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (LoggedInUser == null) throw new ApiException("You are not signed in.");
                    return await _stadiumDashboardService.AddStadiumWorkingDays(dto, LoggedInUser.Id, processId);
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<bool> AddPaymentInfo([FromBody] DashboardPaymentInfoDto dto, long processId)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not signed in.");
                var paumentAdded = await _stadiumDashboardService.AddPaymentInfo(dto, LoggedInUser.Id, processId);
                return paumentAdded;
                //if (paumentAdded)
                //{
                //    return await _stadiumDashboardService.CompleteCreationProcess(LoggedInUser.Id, processId);
                //}
                //else
                //{
                //    //return false;
                //    throw new ApiException("something went wrong", 500);
                //}
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<List<FloorDto>> GetSportsFloors(long sportId)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not signed in.");
                return await _stadiumDashboardService.GetSportsFloors(sportId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<List<ServiceDto>> GetServices()
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not signed in.");
                return await _stadiumDashboardService.GetServices(CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        

        

        

        

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //[HttpGet]
        //public async Task<long> CreateStadium(DashboardStadiumDto dto, long processId)
        //{
        //    try
        //    {
        //        if (LoggedInUser == null) throw new ApiException("You are not signed in.");
        //        return await _stadiumDashboardService.CreateStadium(dto, LoggedInUser.Id, processId);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        

        

        
    }
}
