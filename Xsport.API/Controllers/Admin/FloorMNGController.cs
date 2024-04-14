using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Xsport.Core.MNGServices.FloorMNGServices;
using Xsport.DTOs.CourseDtos.MNGDtos;
using Xsport.DTOs.FloorDtos.MNGDtos;

namespace Xsport.API.Controllers.Admin
{
    [ApiController]
    [Route("api/dashboard/[controller]/[action]")]
    public class FloorMNGController : BaseController
    {
        private IFloorMNGService _floorMNGService { get; set; }
        public FloorMNGController(IFloorMNGService floorMNGService)
        {
            _floorMNGService = floorMNGService;
        }

        [HttpPost]
        public async Task<long> CreateFloor([FromBody] FloorDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await _floorMNGService.CreateFloor(dto);
                }
                catch (Exception ex)
                {
                    throw new ApiException(ex.Message, 500);
                }
            }
            else
            {
                throw new ApiException("Invalid Inputs.", 500);
            }
        }
    }
}
