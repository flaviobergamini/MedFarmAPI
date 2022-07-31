using MedFarmAPI.Data;
using MedFarmAPI.Models;
using MedFarmAPI.ValidateModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class OrderController:ControllerBase
    {
        [HttpPost("createOrder")]
        public async Task<IActionResult> PostAsync([FromBody] OrderValidateModel order, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var client = await context.Clients.FirstOrDefaultAsync(x => x.Id == order.ClientId);
            if (client == null)
                return BadRequest();

            var drugstore = await context.Drugstores.FirstOrDefaultAsync(x => x.Id == order.DrugstoresId);
            if (drugstore == null)
                return BadRequest();

            var model = new Order
            {
                Id = order.Id,
                Image = order.Image,
                State = order.State,
                City = order.City,
                Complement = order.Complement,
                Cep = order.Cep,
                Street = order.Street,
                StreetNumber = order.StreetNumber,
                Client = client,
                Drugstores = drugstore
            };

            await context.Orders.AddAsync(model);
            await context.SaveChangesAsync();
            return Created($"v1/createOrder/{model.Id}", model);
        }
    }
}
