using System.ComponentModel;
using System.Security.Claims;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Swashbuckle.AspNetCore.Annotations;
using Xsport.Core;
using Xsport.Core.SportServices;
using Xsport.DTOs.UserDtos;

namespace Xsport.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Tags("Sport")]
[ApiExplorerSettings(GroupName = "application")]
public class SportController : BaseController
{
    private ISportServices _sportServices;
    public SportController(ISportServices sportServices)
    {
        _sportServices = sportServices;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<List<SportDto>> GetSports()
    {
        try
        {
            return await _sportServices.GetSports(CurrentLanguageId);
        }
        catch(Exception ex)
        {
            throw new ApiException(ex.Message);
        }
    }
}