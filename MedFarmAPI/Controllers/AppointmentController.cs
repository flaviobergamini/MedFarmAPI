using MedFarmAPI.Data;
using MedFarmAPI.MessageResponseModel;
using MedFarmAPI.MessageResponseModel.AppointmentDoctorResponse;
using MedFarmAPI.Models;
using MedFarmAPI.ValidateModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("v1/appointment")]
    public class AppointmentController:ControllerBase
    {
        [Authorize(Roles = "Client")]
        [HttpPost("client")]
        public async Task<IActionResult> PostAsync(
            [FromBody] AppointmentValidateModel appointment, 
            [FromServices] DataContext context,
            CancellationToken cancellationToken
            )
        {
            if (!ModelState.IsValid)
                return BadRequest(new MessageModel
                {
                    Code = "MFAPI4000",
                    Message = "Invalid appointment"
                });

            Client? client = await context.Clients.FirstOrDefaultAsync(x => x.Id == appointment.ClientId);
            if (client == null)
                return NotFound(new MessageModel
                {
                    Code = "MFAPI4040",
                    Message = "Client not found in Database, Invalid ID"
                });

            Doctor? doctor = await context.Doctors.FirstOrDefaultAsync(x => x.Id == appointment.DoctorId);
            if (doctor == null)
                return NotFound(new MessageModel
                {
                    Code = "MFAPI4041",
                    Message = "Doctor not found in Database, Invalid ID"
                });

            var model = new Appointment
            {
                DateTimeAppointment = appointment.DateTimeAppointment,
                Remote = appointment.Remote,
                VideoCallUrl = appointment.VideoCallUrl,
                Payment = appointment.Payment,
                Client = client,
                Doctor = doctor
            };
            try
            {
                await context.Appointments.AddAsync(model);
                await context.SaveChangesAsync();
                return StatusCode(201, new AppointmentResponse
                {
                    Code = "MFAPI2012",
                    Id = model.Id,
                    ClientId = model.Client.Id,
                    DoctorId = model.Doctor.Id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI5000",
                    Message = "Internal server error when saving query"
                });
            }
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet("doctor/confirmed/{id:int}")]
        public async Task<IActionResult> GetAppointmentConfirmedAsync(
            [FromServices] DataContext context,
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            try
            {
                var appointments = (from ap in context.Appointments.Include(a => a.Doctor).Include(b => b.Client)
                                    where ap.Confirmed == true && ap.Doctor.Id == id
                                    select ap);

                List<AppointmentDoctorResponse> listAppointments = new List<AppointmentDoctorResponse>();
                AppointmentDoctorResponse appointmentDoctor;

                foreach (var appointment in appointments)
                {
                    appointmentDoctor = new AppointmentDoctorResponse();
                    appointmentDoctor.Name = appointment.Client.Name;
                    appointmentDoctor.Date = appointment.DateTimeAppointment;
                    appointmentDoctor.Remote = appointment.Remote;
                    listAppointments.Add(appointmentDoctor);
                    
                }

                return Ok(new
                {
                    Code = "MFAPI2006",
                    appointments = listAppointments
                });
            }
            catch
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI50012",
                    Message = "Internal server error when fetching a appointment"
                });
            }

        }

        [Authorize(Roles = "Doctor")]
        [HttpGet("doctor/pending/{id:int}")]
        public async Task<IActionResult> GetAppointmentAsync(
            [FromServices] DataContext context,
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            try
            {
                var appointments = (from ap in context.Appointments.Include(a => a.Doctor).Include(b => b.Client)
                                    where ap.Confirmed == false && ap.Doctor.Id == id
                                    select ap.Client.Name);

                return Ok(new
                {
                    Code = "MFAPI2007",
                    appointments = appointments
                });
            }
            catch
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI50013",
                    Message = "Internal server error when fetching a appointment"
                });
            }
        }
    }
}
