using Microsoft.AspNetCore.Mvc;

namespace resevation_be.Controllers;

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
        return "hello";
    }
}