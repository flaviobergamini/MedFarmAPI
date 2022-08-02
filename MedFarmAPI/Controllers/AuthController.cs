using MedFarmAPI.Models;
using MedFarmAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AuthController:ControllerBase
    {
        [HttpPost("login-client")]
        public IActionResult LoginAsync([FromServices] TokenService tokenService)
        {
            var client = new Client { 
                Name = "Flavio Henrique Madureira Bergamini",
                Id = 1,
                Email = "flaviohenrique@gec.inatel.br"
                };
            var token = tokenService.GenerateToken(client);
            return Ok(token);
        }

        [Authorize(Roles = "Client")]
        [HttpGet("client-test")]
        public IActionResult GetUser() => Ok("Client Logado");
    }
}
