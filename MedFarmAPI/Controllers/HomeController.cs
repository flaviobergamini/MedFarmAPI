using Microsoft.AspNetCore.Mvc;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("v1")]
    public class HomeController:ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {

            return Ok("MFAPI2000 - MedFarmAPI 2022 Ok");
        }
    }
}
