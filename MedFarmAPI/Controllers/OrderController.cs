using MedFarmAPI.Data;
using MedFarmAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedFarmAPI.Controllers
{
    public class OrderController:ControllerBase
    {
        [HttpPost("v1/createOrder")]
        public async Task<IActionResult> PostAsync([FromBody] Order order, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
            return Created($"v1/createOrder/{order.Id}", order);
        }
    }
}
