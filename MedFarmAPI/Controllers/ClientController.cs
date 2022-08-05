using MedFarmAPI.Data;
using MedFarmAPI.Models;
using MedFarmAPI.ValidateModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ClientController : ControllerBase
    {
        [HttpGet("clients")]
        public async Task<IActionResult> GetAsync([FromServices] DataContext context)
        {
            try
            {
                var clients = await context.Clients.AsNoTracking().ToListAsync();

                if (clients == null)
                    return NotFound();

                return Ok(clients);
            }
            catch
            {
                return StatusCode(500, "MFAPI5002 - Erro interno no servidor ao buscar cliente");
            }
        }
    }
}
