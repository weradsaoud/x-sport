using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Xsport.Core.MNGServices.CourseMNGServices;
using Xsport.DTOs.AdminDtos;
using Xsport.DTOs.CourseDtos.MNGDtos;

namespace Xsport.API.Controllers.Admin
{
    [ApiController]
    [Route("api/dashboard/[controller]/[action]")]
    public class CourseMNGController : BaseController
    {
        public ICourseMNGService _courseService { get; set; }
        public CourseMNGController(ICourseMNGService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        public async Task<long> CreateCourse([FromBody] CourseDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    return await _courseService.CreateCourse(dto);
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
