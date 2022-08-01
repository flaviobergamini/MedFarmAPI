using MedFarmAPI.Data;
using MedFarmAPI.Models;
using MedFarmAPI.ValidateModels;
using Microsoft.AspNetCore.Mvc;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class DoctorController:ControllerBase 
    {
        [HttpPost("create-doctor")]
        public async Task<IActionResult> PostAsync([FromBody] DoctorValidateModel doctor, [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest("MFAPI4001 - Médico inválido");

            var model = new Doctor
            {
                Name = doctor.Name,
                Email = doctor.Email,
                Phone = doctor.Phone,
                Cpf = doctor.Cpf,
                State = doctor.State,
                City = doctor.City,
                Complement = doctor.Complement,
                Cep = doctor.Cep,
                Street = doctor.Street,
                StreetNumber = doctor.StreetNumber,
                Specialty = doctor.Specialty,
                RegionalCouncil = doctor.RegionalCouncil,
            };
            try { 
                await context.Doctors.AddAsync(model);
                await context.SaveChangesAsync();
                return Created($"v1/create-doctor/{model.Id}", model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "MFAPI5002 - Erro interno no servidor ao cadastrar médico");
            }
        }
    }
}
