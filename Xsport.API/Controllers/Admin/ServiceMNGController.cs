using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Xsport.Core.MNGServices.ServiceMNGServices;
using Xsport.DTOs.ServiceDtos.MNGDtos;

namespace Xsport.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]/[action]")]
    [Tags("ServiceMNG")]
    [ApiExplorerSettings(GroupName = "administration")]
    public class ServiceMNGController : BaseController
    {
        public IServiceMNGService _serviceMNGService { get; set; }
        public ServiceMNGController(IServiceMNGService serviceMNGService)
        {
             _serviceMNGService = serviceMNGService;
        }
        [HttpPost]
        public async Task<long> CreateService([FromForm] PostServiceDto dto)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    return await _serviceMNGService.CreateService(dto);
                }
                catch(Exception ex)
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
        public async Task<List<GetServiceDto>> GetServices()
        {
            try
            {
                return await _serviceMNGService.GetServices();
            }
            catch(Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
    }
}
