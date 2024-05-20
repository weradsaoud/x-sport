using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xsport.Core.DashboardServices.StadiumServices;
using Xsport.DB.Entities;
using Xsport.DTOs.StadiumDtos.DashboardDtos;

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

        [HttpPost]
        public async Task<IActionResult> AddStadiumStepOne([FromBody] StadiumStepOneDto step1DTO)
        {
            var stadiumId = await _stadiumDashboardService.AddStadiumStepOne(step1DTO);
            return Ok(stadiumId);
        }

        [HttpPost("{stadiumId}")]
        public async Task<IActionResult> AddStadiumStepTwo(long stadiumId, [FromBody] StadiumStepTwoDto step2DTO)
        {
            await _stadiumDashboardService.AddStadiumStepTwo(stadiumId, step2DTO);
            return Ok();
        }

        [HttpPost("{stadiumId}")]
        public async Task<IActionResult> AddStadiumStepThree(long stadiumId, [FromBody] StadiumStepThreeDto step3DTO)
        {
            await _stadiumDashboardService.AddStadiumStepThree(stadiumId, step3DTO);
            return Ok();
        }

        // Start a new stadium creation process or retrieve an existing one
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("start")]
        public async Task<IActionResult> StartProcess()
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not signed in.");
                return Ok(await _stadiumDashboardService.StartProcess(LoggedInUser.Uid!));
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }

        // Save current step data
        [HttpPost("saveStep")]
        public async Task<IActionResult> SaveStep([FromBody] StadiumStepDataModel model)
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not signed in.");
                return Ok(await _stadiumDashboardService.SaveStep(model));
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }

        //// Resume process
        //[HttpGet("resume")]
        //public async Task<IActionResult> ResumeProcess(Guid processId, string userId)
        //{
        //    var process = await _context.StadiumCreationProcesses
        //        .FirstOrDefaultAsync(p => p.Id == processId && p.UserId == userId);

        //    if (process == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(process);
        //}

        //// Complete the creation process
        //[HttpPost("complete")]
        //public async Task<IActionResult> CompleteProcess(Guid processId, string userId)
        //{
        //    var process = await _context.StadiumCreationProcesses
        //        .FirstOrDefaultAsync(p => p.Id == processId && p.UserId == userId);

        //    if (process == null)
        //    {
        //        return NotFound();
        //    }

        //    var stadium = process.StadiumData;

        //    _context.Stadiums.Add(stadium);
        //    await _context.SaveChangesAsync();

        //    // Remove the process record
        //    _context.StadiumCreationProcesses.Remove(process);
        //    await _context.SaveChangesAsync();

        //    return Ok(stadium);
        //}
    }
}
