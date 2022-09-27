using MedFarmAPI.Response;
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

            var obj = new
            {
                JwtKey = Configuration.JwtKey,
                ConnectionString = Configuration.Utility.Context,
                SendGrid = "SendGrid",
                Host = Configuration.Smtp.Host,
                Port = Configuration.Smtp.Port,
                UserName = Configuration.Smtp.UserName,
                Password = Configuration.Smtp.Password,
                Firebase = "Firebase",
                ApiKey = Configuration.Firebase.ApiKey,
                Bucket = Configuration.Firebase.Bucket,
                AuthEmail = Configuration.Firebase.AuthEmail,
                AuthPassword = Configuration.Firebase.AuthPassword,
            };

            return Ok(obj);
        }
    }
}
