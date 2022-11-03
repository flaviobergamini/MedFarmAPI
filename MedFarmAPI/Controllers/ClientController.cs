using MedFarmAPI.Data;
using MedFarmAPI.Response;
using MedFarmAPI.Response.ClientResponse;
using MedFarmAPI.Request.ClientResquest;
using MedFarmAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedFarmAPI.ValidateModels;

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

                var appointments = await (from ap in context.Appointments.Include(a => a.Doctor).Include(b => b.Client)
                                          where ap.Client.Id == clients.Id || ap.Client.RefreshToken == clients.RefreshToken
                                          select ap).ToListAsync();

                var orders = await (from order in context.Orders.Include(a => a.Drugstores).Include(b => b.Client)
                                    where order.Client.Id == clients.Id || order.Client.RefreshToken == clients.RefreshToken
                                    select order).ToListAsync();

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

                    var doctors = await context.Doctors?.AsNoTracking()
                    .Where(x => x.Specialty == clientSearchRequest.Specialty && x.City == clientSearchRequest.City).ToListAsync();
                    return Ok(new
                    {
                        Code = "MFAPI2002",
                        Doctors = doctors
                    });

                    break;

                case "Drugstore":
                    var drugstores = await context.Drugstores.AsNoTracking().Where(x => x.City == clientSearchRequest.City).ToListAsync();
                    return Ok(new
                    {
                        Code = "MFAPI2003",
                        Drugstores = drugstores
                    });

                    break;

                default:
                    return BadRequest(new MessageModel
                    {
                        Code = "MFAPI4008",
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

        [Authorize(Roles = "Client")]
        [HttpGet("{id:int}/Appointments")]
        public async Task<IActionResult> ListAppointments(
            [FromServices] DataContext context,
            [FromRoute] int id
            )
        {
            try
            {
                var appointments = await context.Appointments
                    .Include(x => x.Client)
                    .Include(x => x.Doctor)
                    .AsNoTracking()
                    .Where(x => x.Client.Id == id && x.Confirmed == true)
                    .ToListAsync();

                return Ok(new
                {
                    Code = "MFAPI20020",
                    Appointments = appointments
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI50030",
                    Message = "Internal server error when search appointment"
                });
            }
        }

        [Authorize(Roles = "Client")]
        [HttpGet("{id:int}/Orders")]
        public async Task<IActionResult> ListOrders(
            [FromServices] DataContext context,
            [FromRoute] int id
            )
        {
            try
            {
                var orders = await context.Orders
                    .Include(x => x.Client)
                    .Include(x => x.Drugstores)
                    .AsNoTracking()
                    .Where(x => x.Client.Id == id && x.Confirmed == true)
                    .ToListAsync();

                return Ok(new
                {
                    Code = "MFAPI20021",
                    Orders = orders
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI50031",
                    Message = "Internal server error when search appointment"
                });
            }
        }
    }
}
