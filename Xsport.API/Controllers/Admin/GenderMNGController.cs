using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Xsport.Core.MNGServices.GenderMNGServices;
using Xsport.DTOs.GenderDtos.MNGDtos;

namespace Xsport.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]/[action]")]
    [Tags("GenderMNG")]
    [ApiExplorerSettings(GroupName = "administration")]
    public class GenderMNGController : BaseController
    {
        public IGenderMNGService _genderMNGService { get; set; }
        public GenderMNGController(IGenderMNGService genderMNGService)
        {
            _genderMNGService = genderMNGService;
        }
        [HttpPost]
        public async Task<long> CreateGender(GenderDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await _genderMNGService.CreateGender(dto);
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
        [HttpGet]
        public async Task<List<GenderDto>> GetGenders()
        {
            try
            {
                return await _genderMNGService.GetGenders(CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
    }
}
