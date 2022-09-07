using MedFarmAPI.Data;
using MedFarmAPI.Response;
using MedFarmAPI.Response.OrderDrugstoreResponse;
using MedFarmAPI.Models;
using MedFarmAPI.Services;
using MedFarmAPI.ValidateModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("v1/order")]
    public class OrderController:ControllerBase
    {
        private static string imageFirebaseStorage = null;

        [Authorize(Roles = "Client")]
        [HttpPost("client/upload/image")]
        public async Task<IActionResult> PostTesteAsync(
           IFormFile formFile,
           CancellationToken cancellationToken)
        {
            FirebaseStorageService FirebaseService = new FirebaseStorageService();
            imageFirebaseStorage = await FirebaseService.SendImage(formFile);

            if (imageFirebaseStorage != null)
            {
                return StatusCode(201, new
                {
                    Code = "MFAPI2015",
                    ImageFirebaseStorage = imageFirebaseStorage
                });
            }
            else
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI50019",
                    Message = "Could not upload image to Firebase"
                });
            }
        }

        [Authorize(Roles = "Client")]
        [HttpPost("client")]
        public async Task<IActionResult> PostAsync(
            [FromBody] OrderValidateModel order,
            [FromServices] DataContext context,
            CancellationToken cancellationToken
            )
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
                Image = imageFirebaseStorage,
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

            imageFirebaseStorage = null;
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

        [Authorize(Roles = "Drugstore")]
        [HttpGet("drugstore/{id:int}")]
        public async Task<IActionResult> GetOrderFalseAsync(
            [FromRoute] int id,
            [FromServices] DataContext context,
            CancellationToken cancellationToken)
        {
            try
            {
                var orders = await (from order in context.Orders.Include(a => a.Drugstores).Include(b => b.Client)
                                          where order.Confirmed == false && order.Drugstores.Id == id
                                          select order).ToListAsync();

                List<OrderPendingResponse> listOrders = new List<OrderPendingResponse>();
                OrderPendingResponse orderPendingResponse;

                foreach (var order in orders)
                {
                    orderPendingResponse = new OrderPendingResponse();
                    orderPendingResponse.Id = order.Id;
                    orderPendingResponse.Name = order.Client.Name;
                    listOrders.Add(orderPendingResponse);
                }

                return Ok(new
                {
                    Code = "MFAPI2008",
                    appointments = listOrders
                });
            }
            catch
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI50014",
                    Message = "Internal server error when fetching a orders"
                });
            }
        }

        [Authorize(Roles = "Drugstore")]
        [HttpPatch("drugstore/order/{id:int}")]
        public async Task<IActionResult> PatchOrderAsync(
            [FromServices] DataContext context,
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            try
            {
                var order = await context.Orders.FirstOrDefaultAsync(x => x.Id == id);
                if (order == null)
                {
                    return NotFound(new MessageModel
                    {
                        Code = "MFAPI40414",
                        Message = "Order not found in Database, Invalid ID"
                    });
                }
                order.Confirmed = true;
                context.Orders.Update(order);
                await context.SaveChangesAsync();
                return Ok(new MessageModel
                {
                    Code = "MFAPI2009",
                    Message = "Confirmation done"
                });
            }
            catch
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI50016",
                    Message = "Internal server error when updating an order"
                });
            }
        }

        [Authorize(Roles = "Drugstore")]
        [HttpGet("drugstore/order/{id:int}")]
        public async Task<IActionResult> GetOrderConfirmedAsync(
            [FromRoute] int id,
            [FromServices] DataContext context,
            CancellationToken cancellationToken)
        {
            try
            {
                var order = await (from o in context.Orders.Include(b => b.Client)
                                    where o.Confirmed == true && o.Id == id
                                    select o).ToListAsync();

                if(order == null)
                {
                    return NotFound(new MessageModel
                    {
                        Code = "MFAPI40415",
                        Message = "Order not found"
                    });
                }

                OrderClientResponse orderClientResponse = new OrderClientResponse();
                orderClientResponse.Id = order[0].Id;
                orderClientResponse.Name = order[0].Client.Name;
                orderClientResponse.Street = order[0].Client.Street;
                orderClientResponse.District = order[0].Client.District;
                orderClientResponse.City = order[0].Client.City;
                orderClientResponse.StreetNumber = order[0].Client.StreetNumber;
                orderClientResponse.Image = order[0].Image;

                return Ok(new
                {
                    Code = "MFAPI20012",
                    appointments = orderClientResponse
                });
            }
            catch
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI50015",
                    Message = "Internal server error when fetching a order"
                });
            }
        }
    }
}
