using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Xsport.Core.MNGServices.AgeCategoryMNGServices;
using Xsport.DB;
using Xsport.DTOs.AdminDtos;
using Xsport.DTOs.AgeCategoryDtos.MNGDtos;
using Xsport.DTOs.ServiceDtos.MNGDtos;

namespace Xsport.API.Controllers.Admin
{
    [ApiController]
    [Route("api/dashboard/[controller]/[action]")]
    public class AgeCategoryMNGController : BaseController
    {
        public IAgeCategoryMNGService _ageCategoryMNGService { get; set; }
        public AgeCategoryMNGController(IAgeCategoryMNGService ageCategoryMNGService)
        {
            _ageCategoryMNGService = ageCategoryMNGService;
        }

        [HttpPost]
        public async Task<long> CreateAgeCategory([FromBody] AgeCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await _ageCategoryMNGService.CreateAgeCategory(dto);
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
