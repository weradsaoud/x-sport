using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Xsport.Core.ArchiveServices;
using Xsport.DTOs.ArchiveDtos;

namespace Xsport.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ArchiveController : BaseController
    {
        private IArchiveServices _archiveServices { get; set; }
        public ArchiveController(IArchiveServices archiveServices)
        {
            _archiveServices = archiveServices;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<List<AcademyArchiveItem>> AcademiesSubscriptionArchive()
        {
            try
            {
                if (LoggedInUser == null) throw new ApiException("You are not loggedIn", 500);
                return await _archiveServices.AcademiesSubscriptionArchive(LoggedInUser.Id, CurrentLanguageId);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, 500);
            }
        }

    }
}
