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
using Xsport.DTOs.AcademyDtos;
using Xsport.DTOs.CommonDtos;
using Xsport.DTOs.UserDtos;

namespace Xsport.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AcademyController : BaseController
{
    private IAcademyServices _academyServices;
    public AcademyController(IAcademyServices academyServices)
    {
        _academyServices = academyServices;
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<List<UserSportsAcademies>> GetSportsMemberShip()
    {
        try
        {
            if (LoggedInUser == null) throw new ApiException("You are not signed in.");
            return await _academyServices.GetSportsMemberShip(LoggedInUser.Id, CurrentLanguageId);
        }
        catch (Exception ex)
        {
            throw new ApiException(ex.Message);
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<List<SuggestedAcademyDto>> GetSuggestedAcademies([FromQuery] PagingDto dto)
    {
        try
        {
            if (LoggedInUser == null) throw new ApiException("You are not signed in.");
            return await _academyServices.GetSuggestedAcademies(LoggedInUser, dto, CurrentLanguageId);
        }
        catch (Exception ex)
        {
            throw new ApiException(ex.Message);
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<AboutAcademyDto> GetAboutAcademy([FromQuery] long academyId)
    {
        if (ModelState.IsValid)
        {
            try
            {
                return await _academyServices.GetAboutAcademy(academyId, CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
        else
        {
            throw new ApiException("Invalide input.");
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<AcademyCoursesDto> GetAcademyCourses([FromQuery] long academyId)
    {
        if (ModelState.IsValid)
        {
            try
            {
                return await _academyServices.GetAcademyCoursesToday(academyId, CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
        else
        {
            throw new ApiException("Invalid Inputs");
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<AcademyCoursesDto> GetAcademyCoursesInDate([FromQuery] long academyId, [FromQuery] string targetDate)
    {
        if (ModelState.IsValid)
        {
            try
            {
                return await _academyServices.GetAcademyCoursesInDate(
                    academyId, CurrentLanguageId, targetDate);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
        else
        {
            throw new ApiException("Invalid Inputs");
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<AcademyReviewDto> GetAcademyReviews([FromQuery] long academyId)
    {
        if (ModelState.IsValid)
        {
            try
            {
                return await _academyServices.GetAcademyReviews(
                    academyId, CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
        else
        {
            throw new ApiException("Invalid Inputs");
        }
    }

}