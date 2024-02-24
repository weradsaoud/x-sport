using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Xsport.Common.Enums;
using Xsport.DB.Entities;

namespace Xsport.API.Controllers;
public abstract class BaseController : ControllerBase
{
    protected string Culture => Request.Headers["culture"].ToString();
    protected short CurrentLanguageId => (Culture == "ar") ? (short)LanguagesEnum.Arabic : (short)LanguagesEnum.English;
    protected string Uid => User.FindFirst(ClaimTypes.Authentication)?.Value ?? string.Empty;
    protected XsportUser? LoggedInUser =>   HttpContext.RequestServices.GetService<UserManager<XsportUser>>()?.GetUserAsync(HttpContext.User).Result;
}