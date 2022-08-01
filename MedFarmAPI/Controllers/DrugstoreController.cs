using MedFarmAPI.Data;
using MedFarmAPI.Models;
using MedFarmAPI.ValidateModels;
using Microsoft.AspNetCore.Mvc;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class DrugstoreController:ControllerBase
    {
        [HttpPost("create-drugstore")]
        public async Task<IActionResult> PostAsync([FromBody] DrugstoreValidateModel drugstore, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest("MFAPI4002 - Farmácia inválida");

            var model = new Drugstore
            {
                Name = drugstore.Name,
                Email = drugstore.Email,
                Phone = drugstore.Phone,
                Cnpj = drugstore.Cnpj,
                State = drugstore.State,
                City = drugstore.City,
                Complement = drugstore.Complement,
                Cep = drugstore.Cep,
                Street = drugstore.Street,
                StreetNumber = drugstore.StreetNumber
            };
            try
            {
                await context.Drugstores.AddAsync(model);
                await context.SaveChangesAsync();
                return Created($"v1/create-drugstore/{model.Id}", model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "MFAPI5003 - Erro interno no servidor ao cadastrar farmácia");
            }
        }
    }
}
