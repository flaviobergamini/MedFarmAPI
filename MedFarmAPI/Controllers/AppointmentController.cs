using MedFarmAPI.Data;
using MedFarmAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedFarmAPI.Controllers
{
    public class AppointmentController:ControllerBase
    {
        [HttpPost("v1/createAppointment")]
        public async Task<IActionResult> PostAsync([FromBody] Appointment appointment, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await context.Appointments.AddAsync(appointment);
            await context.SaveChangesAsync();
            return Created($"v1/createAppointment/{appointment.Id}", appointment);
        }
    }
}
