﻿using MedFarmAPI.Data;
using MedFarmAPI.Models;
using MedFarmAPI.ValidateModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AppointmentController:ControllerBase
    {
        [HttpPost("createAppointment")]
        public async Task<IActionResult> PostAsync([FromBody] AppointmentValidateModel appointment, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var client = await context.Clients.FirstOrDefaultAsync(x => x.Id == appointment.ClientId);
            if(client == null)
                return BadRequest();

            var doctor = await context.Doctors.FirstOrDefaultAsync(x => x.Id == appointment.DoctorId);
            if (doctor == null)
                return BadRequest();

            var model = new Appointment
            {
                Id = appointment.Id,
                DateTimeAppointment = appointment.DateTimeAppointment,
                Remote = appointment.Remote,
                VideoCallUrl = appointment.VideoCallUrl,
                Client = client,
                Doctor = doctor
            };

            await context.Appointments.AddAsync(model);
            await context.SaveChangesAsync();
            return Created($"v1/createAppointment/{model.Id}", model);
        }
    }
}
