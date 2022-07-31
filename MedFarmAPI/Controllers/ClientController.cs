using MedFarmAPI.Data;
using MedFarmAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase 
    {
        [HttpPost("v1/createClient")]
        public async Task<IActionResult> PostAsync([FromBody] Client client, [FromServices] DataContext context)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await context.Clients.AddAsync(client);
            await context.SaveChangesAsync();
            return Created($"v1/createClient/{client.Id}", client);
        }
    }
}
