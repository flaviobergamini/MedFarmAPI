using MedFarmAPI.Data;
using MedFarmAPI.MessageResponseModel;
using MedFarmAPI.MessageResponseModel.ClientResponse;
using MedFarmAPI.MessageResponseModel.ClientResponse.Model;
using MedFarmAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ClientController : ControllerBase
    {

        [Authorize(Roles = "Client")]
        [HttpPost("client-logged")]
        public async Task<IActionResult> PostAsync(
            [FromServices] DataContext context,
            [FromBody] ClientLoggedModel clientLoggedModel,
            CancellationToken cancellationToken
            )
        {
            if (!ModelState.IsValid)
                return BadRequest(new MessageModel
                {
                    Code = "MFAPI4005",
                    Message = "Invalid client logged in"
                });

            List<ClientLoggedDoctorResponse> doctorAppointments = new List<ClientLoggedDoctorResponse>();
            List<ClientLoggedOrderResponse> drugstoreOrders = new List<ClientLoggedOrderResponse>();

            try
            {
                Client? clients = await context.Clients.AsNoTracking().Include(a => a.Appointments).Include(b => b.Orders)
                   .FirstOrDefaultAsync(x => x.Id == clientLoggedModel.Id);

                if (clients == null)
                    return NotFound(new MessageModel
                    {
                        Code = "MFAPI40410",
                        Message = "Client not found in Database, Invalid ID"
                    });

                Order? orders = await context.Orders.AsNoTracking().Include(a => a.Drugstores).Include(b => b.Client)
                   .FirstOrDefaultAsync(x => x.Client.Id == clients.Id);

                Appointment? appointments = await context.Appointments.AsNoTracking().Include(a => a.Doctor).Include(b => b.Client)
                   .FirstOrDefaultAsync(x => x.Client.Id == clients.Id);
                
                ClientLoggedDoctorResponse doctorResponse;
                ClientLoggedOrderResponse orderResponse;
                
                for( var i = 0; i < clients.Appointments.Count; i++)
                {
                    doctorResponse = new ClientLoggedDoctorResponse();
                    doctorResponse.Name = appointments.Doctor.Name;
                    doctorResponse.Street = appointments.Doctor.Street;
                    doctorResponse.DoctorSpecialty = appointments.Doctor.Specialty;
                    doctorResponse.DoctorDateTimeAppointment = appointments.DateTimeAppointment;

                    doctorAppointments.Add(doctorResponse);
                }

                for (var i = 0; i < clients.Orders.Count; i++)
                {
                    orderResponse = new ClientLoggedOrderResponse();
                    orderResponse.Name = orders.Drugstores.Name;
                    orderResponse.Street = orders.Drugstores.Street;
                    orderResponse.DrugstoreDateTimeOrder = orders.DateTimeOrder; 

                    drugstoreOrders.Add(orderResponse);
                }

                var requests = new {
                    Code = "MFAPI2001",
                    doctorList = doctorAppointments,
                    drugstoreList = drugstoreOrders
                };

                return Ok(requests); 
            }
            catch
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI50011",
                    Message = "Internal server error when fetching a client"
                });
            }
        }
    }
}
