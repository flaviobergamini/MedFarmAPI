using MedFarmAPI.Data;
using MedFarmAPI.MessageResponseModel;
using MedFarmAPI.MessageResponseModel.AppointmentDoctorResponse;
using MedFarmAPI.Models;
using MedFarmAPI.Models.AppointmentDoctorRequest;
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
                    Code = "MFAPI2014",
                    Id = model.Id,
                    ClientId = model.Client.Id,
                    DoctorId = model.Doctor.Id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI50018",
                    Message = "Internal server error when search appointment"
                });
            }
        }

        [Authorize(Roles = "Client")]
        [HttpGet("client/doctor/datetime/{id:int}")]
        public async Task<IActionResult> GetAsync(
            [FromRoute] int id,
            [FromServices] DataContext context,
            CancellationToken cancellationToken
            )
        {
            try
            {
                var appointments = await (from ap in context.Appointments.Include(a => a.Doctor).Include(b => b.Client)
                                          where ap.Confirmed == true && ap.Doctor.Id == id
                                          select ap).ToListAsync();

                List<string> dayTime = new List<string>();
                List<string> dateWeek = new List<string>();
                string timeString;
                string dateString = "";

                var dateTimeUtc = DateTime.UtcNow;
                var brasilia = TimeZoneInfo.FindSystemTimeZoneById("Brazil/East");
                var dateTimeBrazil = String.Format(TimeZoneInfo.ConvertTimeFromUtc(dateTimeUtc, brasilia).ToString());

                int day = int.Parse(dateTimeBrazil.Substring(0, 2));
                int month = int.Parse(dateTimeBrazil.Substring(3, 2));
                int year = int.Parse(dateTimeBrazil.Substring(6, 4));

                DateTime dateTime = new DateTime(year, month, day);
                DateTime dateTimeVerify;

                int numberDay = (int)dateTime.DayOfWeek;
                int dayAccount, dayVerify;
                short Sunday = 0, Monday = 1, Saturday = 6;

                if (numberDay > Sunday && numberDay < Saturday)
                {
                    dayAccount = 0;
                    for (var i = Monday; i < Saturday; i++)
                    {
                        dateTimeVerify = new DateTime(year, month, day+dayAccount);
                        dayVerify = (int)dateTimeVerify.DayOfWeek;
                        if (dayVerify > Sunday && dayVerify < Saturday)
                        {
                            if (month < 10)
                                if (day < 10)
                                    dateString = $"0{day+dayAccount}/0{month}/{year}";
                                else
                                    dateString = $"{day+dayAccount}/0{month}/{year}";
                            else
                                if (day < 10)
                                dateString = $"0{day+dayAccount}/{month}/{year}";
                            else
                                dateString = $"{day+dayAccount}/{month}/{year}";
                            dateWeek.Add(dateString);
                            dayAccount++;
                        }
                    }
                }
                else
                {
                    dayAccount = Monday;
                    for (var i = Monday; i < Saturday; i++)
                    {
                        if (month < 10)
                            if (day < 10)
                                dateString = $"0{day+dayAccount}/0{month}/{year}";
                            else
                                dateString = $"{day+dayAccount}/0{month}/{year}";
                        else
                            if (day < 10)
                            dateString = $"0{day+dayAccount}/{month}/{year}";
                        else
                            dateString = $"{day+dayAccount}/{month}/{year}";
                        dateWeek.Add(dateString);
                        dayAccount++;
                    }
                }

                for (var i = 8; i < 19; i++)
                {
                        if(i < 10)
                            timeString = $"0{i}:00";
                        else
                            timeString = $"{i}:00";
                        dayTime.Add(timeString);
                }

                List<string> appointmentDateTimePending = new List<string>();
                string appointmentDate, appointmentTime;

                foreach (var date in dateWeek)
                {
                    foreach(var time in dayTime)
                    {
                        appointmentDateTimePending.Add($"{date} {time}");
                    }
                }

                foreach (var appointment in appointments)
                {
                    appointmentDate = appointment.DateTimeAppointment.ToString().Substring(0, 10);
                    appointmentTime = appointment.DateTimeAppointment.ToString().Substring(11, 5);

                    appointmentDateTimePending.Remove($"{appointmentDate} {appointmentTime}");
                }


                return Ok(new
                {
                    Code = "MFAPI20014",
                    appointmentDateTimePending = appointmentDateTimePending
                }); ;
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
        [HttpPost("doctor/confirmed")]
        public async Task<IActionResult> GetAppointmentConfirmedAsync(
            [FromServices] DataContext context,
            [FromBody] AppointmentConfirmedRequest appointmentConfirmedRequest,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(new MessageModel
                {
                    Code = "MFAPI4009",
                    Message = "Invalid request. Make sure the ID is correct or the date is in the format yyyy-mm-dd"
                });

            try
            {
                var appointments = await (from ap in context.Appointments.Include(a => a.Doctor).Include(b => b.Client)
                                    where ap.Confirmed == true && ap.Doctor.Id == appointmentConfirmedRequest.Id
                                          select ap).ToListAsync();

                List<AppointmentConfirmedResponse> listAppointments = new List<AppointmentConfirmedResponse>();
                AppointmentConfirmedResponse appointmentDoctor;

                foreach (var appointment in appointments)
                {
                   if ((appointment.DateTimeAppointment.ToString()).Substring(0, 10) == appointmentConfirmedRequest.Date.ToString().Substring(0, 10))
                    {
                        appointmentDoctor = new AppointmentConfirmedResponse();
                        appointmentDoctor.Name = appointment.Client.Name;
                        appointmentDoctor.Date = appointment.DateTimeAppointment;
                        appointmentDoctor.Remote = appointment.Remote;
                        listAppointments.Add(appointmentDoctor);
                    }
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
                var appointments = await (from ap in context.Appointments.Include(a => a.Doctor).Include(b => b.Client)
                                    where ap.Confirmed == false && ap.Doctor.Id == id
                                    select ap).ToListAsync();

                List<AppointmentPendingResponse> listAppointments = new List<AppointmentPendingResponse>();
                AppointmentPendingResponse appointmentPendingResponse;

                foreach(var appointment in appointments)
                {
                    appointmentPendingResponse = new AppointmentPendingResponse();
                    appointmentPendingResponse.Id = appointment.Id;
                    appointmentPendingResponse.Name = appointment.Client.Name;
                    appointmentPendingResponse.Date = appointment.DateTimeAppointment;
                    listAppointments.Add(appointmentPendingResponse);
                }

                return Ok(new
                {
                    Code = "MFAPI2007",
                    appointments = listAppointments
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

        [Authorize(Roles = "Doctor")]
        [HttpPatch("doctor/requests/{id:int}")]
        public async Task<IActionResult> PatchAppointmentAsync(
            [FromServices] DataContext context,
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            try
            {
                var appointment = await context.Appointments.FirstOrDefaultAsync(x => x.Id == id);
                if (appointment == null)
                {
                    return NotFound(new MessageModel
                    {
                        Code = "MFAPI40413",
                        Message = "Appointment not found in Database, Invalid ID"
                    });
                }
                appointment.Confirmed = true;
                context.Appointments.Update(appointment);
                await context.SaveChangesAsync();
                return Ok(new MessageModel
                {
                    Code = "MFAPI20013",
                    Message = "Confirmation done"
                });
            }
            catch
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI50017",
                    Message = "Internal server error when updating an appointment"
                });
            }
        }
    }
}
