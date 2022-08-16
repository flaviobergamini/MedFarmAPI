using MedFarmAPI.Data;
using MedFarmAPI.MessageResponseModel;
using MedFarmAPI.MessageResponseModel.ClientResponse;
using MedFarmAPI.Models.ClientResquest;
using MedFarmAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("v1/client")]
    public class ClientController : ControllerBase
    {

        [Authorize(Roles = "Client")]
        [HttpPost("history")]
        public async Task<IActionResult> PostHistoryAsync(
            [FromServices] DataContext context,
            [FromBody] ClientHistoryRequest clientHistoryRequest,
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
                   .FirstOrDefaultAsync(x => x.Id == clientHistoryRequest.Id);

                if (clients == null)
                    return NotFound(new MessageModel
                    {
                        Code = "MFAPI40410",
                        Message = "Client not found in Database, Invalid ID"
                    });

                var appointments = (from ap in context.Appointments.Include(a => a.Doctor).Include(b => b.Client)
                                    where ap.Client.Id == clients.Id || ap.Client.RefreshToken == clients.RefreshToken
                                    select ap);

                var orders = (from order in context.Orders.Include(a => a.Drugstores).Include(b => b.Client)
                              where order.Client.Id == clients.Id || order.Client.RefreshToken == clients.RefreshToken
                              select order);

                ClientLoggedDoctorResponse doctorResponse;
                ClientLoggedOrderResponse orderResponse;

                foreach (var appointment in appointments)
                {
                    doctorResponse = new ClientLoggedDoctorResponse();
                    doctorResponse.Name = appointment.Doctor.Name;
                    doctorResponse.Street = appointment.Doctor.Street;
                    doctorResponse.DoctorSpecialty = appointment.Doctor.Specialty;
                    doctorResponse.DoctorDateTimeAppointment = appointment.DateTimeAppointment;

                    doctorAppointments.Add(doctorResponse);
                }

                foreach (var order in orders)
                {
                    orderResponse = new ClientLoggedOrderResponse();
                    orderResponse.Name = order.Drugstores.Name;
                    orderResponse.Street = order.Drugstores.Street;
                    orderResponse.DrugstoreDateTimeOrder = order.DateTimeOrder;

                    drugstoreOrders.Add(orderResponse);
                }

                return Ok(new
                {
                    Code = "MFAPI2001",
                    doctorList = doctorAppointments,
                    drugstoreList = drugstoreOrders
                });
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

        [Authorize(Roles = "Client")]
        [HttpPost("search")]
        public async Task<IActionResult> PostSearchAsync(
            [FromServices] DataContext context,
            [FromBody] ClientSearchRequest clientSearchRequest,
            CancellationToken cancellationToken
            )
        {
            if (!ModelState.IsValid)
                return BadRequest(new MessageModel
                {
                    Code = "MFAPI4006",
                    Message = "Invalid client logged in"
                });

            switch (clientSearchRequest.Category)
            {
                case "Doctor":
                    if (clientSearchRequest.Specialty.Length == 0 || clientSearchRequest.Specialty == null)
                        return BadRequest(new MessageModel
                        {
                            Code = "MFAPI4007",
                            Message = "Invalid Specialty"
                        });

                    var doctors = context.Doctors?.AsNoTracking()
                    .Where(x => x.Specialty == clientSearchRequest.Specialty && x.City == clientSearchRequest.City).ToList();
                    return Ok(new
                    {
                        Code = "MFAPI2002",
                        Doctors = doctors
                    });

                    break;

                case "Drugstore":
                    var drugstores = context.Drugstores.AsNoTracking().Where(x => x.City == clientSearchRequest.City);
                    return Ok(new
                    {
                        Code = "MFAPI2003",
                        Drugstores = drugstores
                    });

                    break;

                default:
                    return BadRequest(new MessageModel
                    {
                        Code = "MFAPI4007",
                        Message = "Invalid category. Send only, Doctor or Drugstore"
                    });
                    break;
            }
        }

        [Authorize(Roles = "Client")]
        [HttpGet("doctor/{id:int}")]
        public async Task<IActionResult> GetDoctorAsync(
            [FromServices] DataContext context,
            [FromRoute] int id,
            CancellationToken cancellationToken
            )
        {
            var doctor = await context.Doctors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (doctor == null)
                return NotFound(new MessageModel
                {
                    Code = "MFAPI40411",
                    Message = "Doctor not found in Database, Invalid ID"
                });
            else
            {
                return Ok(new
                {
                    Code = "MFAPI2004",
                    Doctor = new
                    {
                        Name = doctor.Name,
                        Street = doctor.Street,
                        District = doctor.District,
                        City = doctor.City,
                        StreetNumber = doctor.StreetNumber
                    }
                });
            }
        }

        [Authorize(Roles = "Client")]
        [HttpGet("drugstore/{id:int}")]
        public async Task<IActionResult> GetDrugstoreAsync(
            [FromServices] DataContext context,
            [FromRoute] int id,
            CancellationToken cancellationToken
            )
        {
            var drugstore = await context.Drugstores.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (drugstore == null)
                return NotFound(new MessageModel
                {
                    Code = "MFAPI40412",
                    Message = "Drugstore not found in Database, Invalid ID"
                });
            else
            {
                return Ok(new
                {
                    Code = "MFAPI2005",
                    Drugstore = new
                    {
                        Name = drugstore.Name,
                        Street = drugstore.Street,
                        District = drugstore.District,
                        City = drugstore.City,
                        StreetNumber = drugstore.StreetNumber
                    }
                });
            }
        }
    }
}
