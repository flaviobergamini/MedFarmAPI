using MedFarmAPI.Data;
using MedFarmAPI.MessageResponseModel;
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
                return BadRequest(new MessageModel
                {
                    Code = "MFAPI4004",
                    Message = "Invalid order"
                });

            Client? client = await context.Clients.FirstOrDefaultAsync(x => x.Id == order.ClientId);
            if (client == null)
                return NotFound(new MessageModel
                {
                    Code = "MFAPI4048",
                    Message = "Client not found in Database, Invalid ID"
                });

            Drugstore? drugstore = await context.Drugstores.FirstOrDefaultAsync(x => x.Id == order.DrugstoresId);
            if (drugstore == null)
                return NotFound(new MessageModel
                {
                    Code = "MFAPI4049",
                    Message = "Drugstore not found in Database, Invalid ID"
                });

            var model = new Order
            {
                Image = order.Image,
                State = order.State,
                City = order.City,
                Complement = order.Complement,
                Cep = order.Cep,
                Street = order.Street,
                StreetNumber = order.StreetNumber,
                DateTimeOrder = order.DateTimeOrder,
                District = order.District,
                Payment = order.Payment,
                Client = client,
                Drugstores = drugstore,
            };

            try
            {
                await context.Orders.AddAsync(model);
                await context.SaveChangesAsync();
                return StatusCode(201, new OrderResponse
                {
                    Code = "MFAPI2013",
                    Id = model.Id,
                    ClientId = model.Client.Id,
                    DrugstoreId = model.Drugstores.Id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI50010",
                    Message = "Internal server error when registering order"
                });
            }
        }

        [HttpGet("order")]
        public async Task<IActionResult> GetAsync([FromServices] DataContext context)
        {
            try
            {
                var orders = await context.Orders.AsNoTracking().ToListAsync();

                if (orders == null)
                    return NotFound();

                return Ok(orders);
            }
            catch
            {
                return StatusCode(500, "MFAPI5002 - Erro interno no servidor ao buscar cliente");
            }
        }
    }
}
