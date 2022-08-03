using MedFarmAPI.Data;
using MedFarmAPI.Models;
using MedFarmAPI.ValidateModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("v1/doctor")]
    public class DoctorController:ControllerBase 
    {
        [HttpGet("doctor")]
        public async Task<IActionResult> GetAsync([FromServices] DataContext context)
        {
            try
            {
                var doctor = await context.Doctors.ToListAsync();

                if (doctor == null)
                    return NotFound();

                return Ok(doctor);
            }
            catch
            {
                return StatusCode(500, "MFAPI5002 - Erro interno no servidor ao buscar cliente");
            }
        }
    }
}
