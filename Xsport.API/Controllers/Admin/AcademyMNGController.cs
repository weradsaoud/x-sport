using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Xsport.Core.MNGServices.AcademyMNGServices;
using Xsport.Core.MNGServices.ServiceMNGServices;
using Xsport.DTOs.AcademyDtos.MNGDtos;
using Xsport.DTOs.AdminDtos;
using Xsport.DTOs.ServiceDtos.MNGDtos;

namespace Xsport.API.Controllers.Admin
{
    [ApiController]
    [Route("api/dashboard/[controller]/[action]")]
    public class AcademyMNGController : BaseController
    {
        public IAcademyMNGService _academyMNGService { get; set; }
        public AcademyMNGController(IAcademyMNGService academyMNGService)
        {
            _academyMNGService = academyMNGService;
        }

        [HttpPost]
        public async Task<long> CreateAcademy([FromBody] PostAcademyDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await _academyMNGService.CreateAcademy(dto);
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
        public async Task<bool> AddAcademyMultimedia([FromForm] AcademyMultimediaDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await _academyMNGService.AddAcademyMultimedia(dto);
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
        public async Task<bool> AddAcademyServices([FromBody] AcademyServicesDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await _academyMNGService.AddAcademyServices(dto);
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
        public async Task<bool> AddAcademyWorkingDays([FromBody] AcademyWorkingDaysDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await _academyMNGService.AddAcademyWorkingDays(dto);
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
