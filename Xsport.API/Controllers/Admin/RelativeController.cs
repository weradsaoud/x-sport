using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Xsport.Core.MNGServices.RelativeMNGServices;
using Xsport.DTOs.FloorDtos.MNGDtos;
using Xsport.DTOs.RelativeDtos.MNGDtos;

namespace Xsport.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]/[action]")]
    [Tags("Relative")]
    [ApiExplorerSettings(GroupName = "administration")]
    public class RelativeController : BaseController
    {
        public IRelativeMNGService _relativeMNGService { get; set; }
        public RelativeController(IRelativeMNGService relativeMNGService)
        {
            _relativeMNGService = relativeMNGService;
        }
        [HttpPost]
        public async Task<long> CreateRelative([FromBody] RelativeDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await _relativeMNGService.CreateRelative(dto);
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
        public async Task<List<RelativeDto>> GetRelatives()
        {
            try
            {
                return await _relativeMNGService.GetRelatives();
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }

    }
}
