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

namespace Xsport.API.Controllers.Admin;

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
}