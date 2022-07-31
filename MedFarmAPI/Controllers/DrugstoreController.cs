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
        [HttpPost("createDrugstore")]
        public async Task<IActionResult> PostAsync([FromBody] DrugstoreValidateModel drugstore, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = new Drugstore
            {
                Id = drugstore.Id,
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

            await context.Drugstores.AddAsync(model);
            await context.SaveChangesAsync();
            return Created($"v1/createDrugstore/{model.Id}", model);
        }
    }
}
