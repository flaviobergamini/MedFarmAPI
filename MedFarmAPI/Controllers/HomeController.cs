using MedFarmAPI.MessageResponseModel;
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

            return Ok(new MessageModel 
            {
                Code = "MFAPI2000",
                Message = "MedFarmAPI 2022 Ok"
            });
        }
    }
}
