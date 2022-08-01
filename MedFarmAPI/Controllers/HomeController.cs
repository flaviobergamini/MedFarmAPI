using Microsoft.AspNetCore.Mvc;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController:ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {

            return Ok("MFAPI2000 - MedFarmAPI 2022 Ok");
        }
    }
}
