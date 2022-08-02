using MedFarmAPI.Models;
using MedFarmAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AuthController:ControllerBase
    {
        [HttpPost("login-client")]
        public async Task<IActionResult> LoginAsync([FromServices] TokenService tokenService)
        {
            var client = new Client();
            var token = tokenService.GenerateToken(client);
            return Ok(token);
        }
    }
}
