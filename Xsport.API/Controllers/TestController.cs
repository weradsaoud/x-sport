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
    public string Test()
    {
        foreach (var item in User.Claims)
        {
            
        }
        return "hello";
    }
}