using System.ComponentModel;
using System.Security.Claims;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Swashbuckle.AspNetCore.Annotations;
using Xsport.Core;
using Xsport.Core.AcademyServices;
using Xsport.Core.SportServices;
using Xsport.DTOs;
using Xsport.DTOs.AcademyDtos;
using Xsport.DTOs.AgeCategoryDtos;
using Xsport.DTOs.CommonDtos;
using Xsport.DTOs.GenderDtos;
using Xsport.DTOs.UserDtos;

namespace Xsport.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Tags("Academy")]
[ApiExplorerSettings(GroupName = "application")]
public class AcademyController : BaseController
{
    private IAcademyServices _academyServices;
    public AcademyController(IAcademyServices academyServices)
    {
        _academyServices = academyServices;
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<List<SubscribedAcademyDto>> GetMemberShips()
    {
        try
        {
            if (LoggedInUser == null) throw new ApiException("You are not signed in.");
            return await _academyServices.GetMemberShips(LoggedInUser.Id, CurrentLanguageId);
        }
        catch (Exception ex)
        {
            throw new ApiException(ex.Message);
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<SuggestedAcademiesDto> GetSuggestedAcademies(
        [FromQuery] long sportId, [FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        try
        {
            if (LoggedInUser == null) throw new ApiException("You are not signed in.", 500);
            PagingDto dto = new PagingDto()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return await _academyServices.GetSuggestedAcademies(
                LoggedInUser, sportId, dto, CurrentLanguageId);
        }
        catch (Exception ex)
        {
            throw new ApiException(ex.Message, 500);
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<SuggestedAcademiesDto> GetAllAcademies([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        try
        {
            if (LoggedInUser == null) throw new ApiException("You are not signed in.", 500);
            PagingDto dto = new PagingDto()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return await _academyServices.GetAllAcademies(
                LoggedInUser, dto, CurrentLanguageId);
        }
        catch (Exception ex)
        {
            throw new ApiException(ex.Message, 500);
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
    public async Task<List<AgeCategoryWithCoursesDto>> GetAcademyCourses([FromQuery] long academyId)
    {
        if (ModelState.IsValid)
        {
            try
            {
                return await _academyServices.GetAcademyCourses(academyId, CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
        else
        {
            throw new ApiException("Invalid Inputs", 500);
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<List<DropDownDto>> GetAgeCategories([FromQuery] long academyId)
    {
        if (ModelState.IsValid)
        {
            try
            {
                return await _academyServices.GetAgeCategories(academyId, CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
        else
        {
            throw new ApiException("Invalid Inputs", 500);
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<List<DropDownDto>> GetGenders()
    {
        if (ModelState.IsValid)
        {
            try
            {
                return await _academyServices.GetGenders(CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
        else
        {
            throw new ApiException("Invalid Inputs", 500);
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<List<DropDownDto>> GetRelatives()
    {
        if (ModelState.IsValid)
        {
            try
            {
                return await _academyServices.GetRelatives(CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
        else
        {
            throw new ApiException("Invalid Inputs", 500);
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<List<AgeCategoryCourseDto>> GetCoursesToSubscribe(
            long academyId, long ageCategoryId, long genderId)
    {
        if (ModelState.IsValid)
        {
            try
            {
                return await _academyServices.GetCoursesToSubscribe(academyId, ageCategoryId, genderId, CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }
        else
        {
            throw new ApiException("Invalid Inputs", 500);
        }
    }
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[HttpGet]
    //public async Task<AcademyCoursesDto> GetAcademyCoursesInDate([FromQuery] long academyId, [FromQuery] string targetDate)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            return await _academyServices.GetAcademyCoursesInDate(
    //                academyId, CurrentLanguageId, targetDate);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new ApiException(ex.Message);
    //        }
    //    }
    //    else
    //    {
    //        throw new ApiException("Invalid Inputs");
    //    }
    //}

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