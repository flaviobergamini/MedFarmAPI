using MedFarmAPI.Data;
using MedFarmAPI.Models;
using MedFarmAPI.ValidateModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("v1/order")]
    public class OrderController:ControllerBase
    {
        [HttpPost("create-order")]
        public async Task<IActionResult> PostAsync([FromBody] OrderValidateModel order, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest("MFAPI4004 - Pedido inválido");

            Client? client = await context.Clients.AsNoTracking().FirstOrDefaultAsync(x => x.Id == order.ClientId);
            if (client == null)
                return NotFound("MFAPI4042 - Cliente não encontrado no Banco de Dados, ID inválido");

            Drugstore? drugstore = await context.Drugstores.AsNoTracking().FirstOrDefaultAsync(x => x.Id == order.DrugstoresId);
            if (drugstore == null)
                return NotFound("MFAPI4043 - Farmácia não encontrada no Banco de Dados, ID inválido");

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

            try
            {
                await context.Orders.AddAsync(model);
                await context.SaveChangesAsync();
                return Created($"v1/create-order/{order.ClientId}", order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "MFAPI5004 - Erro interno no servidor ao cadastrar pedido");
            }
        }
    }
}
