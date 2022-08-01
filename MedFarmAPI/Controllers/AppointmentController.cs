using MedFarmAPI.Data;
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
                return BadRequest("MFAPI4003 - Consulta inválida");

            Client? client = await context.Clients.FirstOrDefaultAsync(x => x.Id == appointment.ClientId);
            if(client == null)
                return NotFound("MFAPI4040 - Cliente não encontrado no Banco de Dados, ID inválido");

            Doctor? doctor = await context.Doctors.FirstOrDefaultAsync(x => x.Id == appointment.DoctorId);
            if (doctor == null)
                return NotFound("MFAPI4041 - Médico não encontrado no Banco de Dados, ID inválido");

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
                return StatusCode(500, "MFAPI5000 - Erro interno no servidor ao salvar consulta");
            }
        }
    }
}
