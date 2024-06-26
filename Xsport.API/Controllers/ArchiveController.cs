﻿using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using Xsport.Core.ArchiveServices;
using Xsport.DTOs.ArchiveDtos;

namespace Xsport.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Tags("Archive")]
    [ApiExplorerSettings(GroupName = "application")]
    public class ArchiveController : BaseController
    {
        private IArchiveServices _archiveServices { get; set; }
        public ArchiveController(IArchiveServices archiveServices)
        {
            _archiveServices = archiveServices;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<List<AcademyArchiveItem>> AcademiesSubscriptionArchive([FromQuery] AcademyArchiveFilter filter)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (LoggedInUser == null) throw new ApiException("You are not loggedIn", 500);
                    return await _archiveServices.AcademiesSubscriptionArchive(LoggedInUser.Id, CurrentLanguageId, filter);
                }
                catch (Exception ex)
                {
                    throw new ApiException(ex.Message, 500);
                }
            }
            else
            {
                throw new ApiException("Invalid Input.");
            }
        }

    }
}
