using MedFarmAPI.Data;
using MedFarmAPI.Models;
using MedFarmAPI.ValidateModels;
using Microsoft.AspNetCore.Mvc;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ClientController : ControllerBase 
    {
        [HttpPost("createClient")]
        public async Task<IActionResult> PostAsync([FromBody] ClientValidateModel client, [FromServices] DataContext context)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = new Client
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
                Phone = client.Phone,
                Cpf = client.Cpf,
                State = client.State,
                City = client.City,
                Complement = client.Complement,
                Cep = client.Cep,
                Street = client.Street,
                StreetNumber = client.StreetNumber
            };

            await context.Clients.AddAsync(model);
            await context.SaveChangesAsync();
            return Created($"v1/createClient/{model.Id}", model);
        }
    }
}
