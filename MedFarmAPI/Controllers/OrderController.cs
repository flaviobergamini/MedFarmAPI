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
        [HttpPost("create-order")]
        public async Task<IActionResult> PostAsync([FromBody] OrderValidateModel order, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Client? client = await context.Clients.FirstOrDefaultAsync(x => x.Id == order.ClientId);
            if (client == null)
                return BadRequest();

            Drugstore? drugstore = await context.Drugstores.FirstOrDefaultAsync(x => x.Id == order.DrugstoresId);
            if (drugstore == null)
                return BadRequest();

            var model = new Order
            {
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
            return Created($"v1/create-order/{order.ClientId}", order);
        }
    }
}
