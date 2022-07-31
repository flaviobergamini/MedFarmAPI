using MedFarmAPI.Data;
using MedFarmAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorController:ControllerBase 
    {
        [HttpPost("v1/createDoctor")]
        public async Task<IActionResult> PostAsync([FromBody] Doctor doctor, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await context.Doctors.AddAsync(doctor);
            await context.SaveChangesAsync();
            return Created($"v1/createDoctor/{doctor.Id}", doctor);
        }
    }
}
