using MedFarmAPI.Data;
using MedFarmAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DrugstoreController:ControllerBase
    {
        [HttpPost("v1/createDrugstore")]
        public async Task<IActionResult> PostAsync([FromBody] Drugstore drugstore, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await context.Drugstores.AddAsync(drugstore);
            await context.SaveChangesAsync();
            return Created($"v1/createDrugstore/{drugstore.Id}", drugstore);
        }
    }
}
