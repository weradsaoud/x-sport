using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Xsport.Core.MNGServices.StadiumMNGServices;
using Xsport.DB;
using Xsport.DB.Entities;
using Xsport.DTOs.StadiumDtos.MNGDtos;

namespace Xsport.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]/[action]")]
    [Tags("StadiumMNG")]
    [ApiExplorerSettings(GroupName = "administration")]
    public class StadiumMNGController : BaseController
    {
        private IStadiumMNGService _stadiumMNGService { get; set; }
        public StadiumMNGController(
            IStadiumMNGService stadiumMNGService)
        {
            _stadiumMNGService = stadiumMNGService;
        }
        [HttpPost]
        public async Task<long> CreateStadium([FromBody]StadiumDto dto)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    return await _stadiumMNGService.CreateStadium(dto);
                }
                catch(Exception ex)
                {
                    throw new ApiException(ex.Message, 500);
                }
            }
            else
            {
                throw new ApiException("Invalid inputs.", 500);
            }
        }
        [HttpPost]
        public async Task<bool> AddStadiumMultimedia([FromForm] StadiumMultimediaDto dto)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    return await _stadiumMNGService.AddStadiumMultimedia(dto);
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
        [HttpPost]
        public async Task<bool> AddStadiumServices([FromBody]StadiumServicesDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await _stadiumMNGService.AddStadiumServices(dto);
                }
                catch(Exception ex)
                {
                    throw new ApiException(ex.Message, 500);
                }
            }
            else
            {
                throw new ApiException("Invalid Input.", 500);
            }
        }
        [HttpPost]
        public async Task<bool> AddStadiumFloor([FromBody] StadiumFloorDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await _stadiumMNGService.AddStadiumFloor(dto);
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
        [HttpPost]
        public async Task<bool> AddStadiumWorkingDays([FromBody] StadiumWorkingDaysDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await _stadiumMNGService.AddStadiumWorkingDays(dto);
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
