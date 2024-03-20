using Microsoft.AspNetCore.Mvc;
using Xsport.Core.StadiumServices;

namespace Xsport.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StadiumController : BaseController
    {
        private IStadiumServices _stadiumService { get; set; }
        public StadiumController(IStadiumServices stadiumService)
        {
            _stadiumService = stadiumService;
        }


    }
}
