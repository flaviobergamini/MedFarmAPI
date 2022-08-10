﻿using MedFarmAPI.Data;
using MedFarmAPI.MessageResponseModel;
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
        [HttpPost("create-appointment")]
        public async Task<IActionResult> PostAsync([FromBody] AppointmentValidateModel appointment, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new MessageModel {
                    Code = "MFAPI4000",
                    Message = "Invalid appointment"
                });

            Client? client = await context.Clients.FirstOrDefaultAsync(x => x.Id == appointment.ClientId);
            if(client == null)
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
                Client = client,
                Doctor = doctor
            };
            try
            {
                await context.Appointments.AddAsync(model);
                await context.SaveChangesAsync();
                return Created($"v1/create-appointment/{model.Id}", model);
            }
            catch (Exception ex) {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI5000",
                    Message = "Internal server error when saving query"
                });
            }
        }

        [HttpGet("appointment")]
        public async Task<IActionResult> GetAsync([FromServices] DataContext context)
        {
            try
            {
                var appointments = await context.Appointments.AsNoTracking().ToListAsync();

                if (appointments == null)
                    return NotFound();

                return Ok(appointments);
            }
            catch
            {
                return StatusCode(500, "MFAPI5002 - Erro interno no servidor ao buscar cliente");
            }
        }
    }
}
