using System.ComponentModel;
using System.Security.Claims;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Xsport.Core;
using Xsport.Core.AcademyServices;
using Xsport.Core.SportServices;
using Xsport.DB.Entities;
using Xsport.DTOs.AcademyDtos;
using Xsport.DTOs.AdminDtos;
using Xsport.DTOs.CommonDtos;
using Xsport.DTOs.UserDtos;

namespace Xsport.API.Controllers;

[ApiController]
[Route("api/dashboard/[controller]/[action]")]
public class AdminController : BaseController
{
    private IAcademyServices _academyServices;
    public AdminController(IAcademyServices academyServices)
    {
        _academyServices = academyServices;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<bool> AddAcademy([FromForm]AddAcademyDto dto)
    {
        try
        {
            return await _academyServices.AddAcademy(dto);
        }
        catch(Exception ex)
        {
            throw new ApiException(ex.Message);
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<bool> AddService([FromBody] AddServiceDto dto)
    {
        try
        {
            return await _academyServices.AddService(dto);
        }
        catch(Exception ex)
        {
            throw new ApiException(ex.Message);
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<bool> AddWorkingDays([FromBody] AddWorkingDaysDto dto)
    {
        try
        {
            return await _academyServices.AddWorkingDays(dto);
        }
        catch (Exception ex)
        {
            throw new ApiException(ex.Message);
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<List<GetWorkingDayDto>> GetWorkingDays()
    {
        try
        {
            return await _academyServices.GetWorkingDays(CurrentLanguageId);
        }
        catch (Exception ex)
        {
            throw new ApiException(ex.Message);
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<List<GetServicesDto>> GetServices()
    {
        try
        {
            return await _academyServices.GetServices(CurrentLanguageId);
        }
        catch (Exception ex)
        {
            throw new ApiException(ex.Message);
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<AgeCategory> AddAgeCategory([FromBody]AddAgeCategoryDto dto)
    {
        try
        {
            return await _academyServices.AddAgeCategory(dto);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<Course> AddCourse([FromBody]AddCourseDto dto)
    {
        try
        {
            return await _academyServices.AddCourse(dto);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<bool> AddServiceToAcademy([FromBody]AddServiceToAcademyDto dto)
    {
        try
        {
            return await _academyServices.AddServiceToAcademy(dto.AcademyId, dto.ServiceValueId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}