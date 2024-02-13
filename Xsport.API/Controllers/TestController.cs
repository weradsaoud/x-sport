using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace Xsport.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TestController : ControllerBase
{

    public TestController()
    {
    }

    [HttpGet]
    public ApiException Test()
    {
        foreach (var item in User.Claims)
        {

        }
        throw new ApiException("it is ok", 500, "1002");
    }
}