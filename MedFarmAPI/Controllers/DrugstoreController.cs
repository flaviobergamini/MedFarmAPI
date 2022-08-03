using MedFarmAPI.Data;
using MedFarmAPI.Models;
using MedFarmAPI.ValidateModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("v1/drugstore")]
    public class DrugstoreController:ControllerBase
    {
        [HttpGet("drugstore")]
        public async Task<IActionResult> GetAsync([FromServices] DataContext context)
        {
            try
            {
                var drugstore = await context.Drugstores.ToListAsync();

                if (drugstore == null)
                    return NotFound();

                return Ok(drugstore);
            }
            catch
            {
                return StatusCode(500, "MFAPI5002 - Erro interno no servidor ao buscar cliente");
            }
        }
    }
}
