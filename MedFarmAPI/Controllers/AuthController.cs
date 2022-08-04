using MedFarmAPI.Data;
using MedFarmAPI.Interfaces;
using MedFarmAPI.Models;
using MedFarmAPI.Services;
using MedFarmAPI.ValidateModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("v1/auth")]
    public class AuthController:ControllerBase
    {
        [HttpPost("create-client")]
        public async Task<IActionResult> PostClientAsync(
            [FromBody] ClientValidateModel client,
            [FromServices] EmailService emailService,
            [FromServices] DataContext context,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest("MFAPI4000 - Usuário inválido");

            var model = new Client
            {
                Name = client.Name,
                Email = client.Email,
                Phone = client.Phone,
                Cpf = client.Cpf,
                State = client.State,
                City = client.City,
                Complement = client.Complement,
                Cep = client.Cep,
                Street = client.Street,
                StreetNumber = client.StreetNumber,
                Password = PasswordHasher.Hash(client.Password),
                Roles = client.Roles
            };
            try
            {
                var confirmEmail = emailService.Send(
                    model.Name,
                    model.Email,
                    "Bem vindo ao Med Farm!!!",
                    "<div style='background:lightgray'>" +
                    "<h1 style='text-align:center; font-size:15pt'> Seja bem vindo ao Med Farm</h1>" +
                    "<h2 style='text-align:center; font-size:10pt'>Conta criada com sucesso</h2>" +
                    "</div>"

                    );

                if (confirmEmail)
                {
                    await context.Clients.AddAsync(model);
                    await context.SaveChangesAsync();
                    return Created($"v1/create-client/{model.Id}", model);
                }
                else
                {
                    return StatusCode(404, "MFAPI4041 - E-mail inválido");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "MFAPI5001 - Erro interno no servidor ao cadastrar cliente");
            }
        }

        [HttpPost("create-doctor")]
        public async Task<IActionResult> PostDoctorAsync(
            [FromBody] DoctorValidateModel doctor, 
            [FromServices] DataContext context,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest("MFAPI4001 - Usuário inválido");

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
                Password = PasswordHasher.Hash(doctor.Password),
                Roles = doctor.Roles,
                Specialty = doctor.Specialty,
                RegionalCouncil = doctor.RegionalCouncil
            };
            try
            {
                await context.Doctors.AddAsync(model);
                await context.SaveChangesAsync();
                return Created($"v1/create-doctor/{model.Id}", model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "MFAPI5002 - Erro interno no servidor ao cadastrar usuário");
            }
        }

        [HttpPost("create-drugstore")]
        public async Task<IActionResult> PostDrugstoreAsync(
            [FromBody] DrugstoreValidateModel drugstore, 
            [FromServices] DataContext context, 
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest("MFAPI4001 - Usuário inválido");

            var model = new Drugstore
            {
                Name = drugstore.Name,
                Email = drugstore.Email,
                Phone = drugstore.Phone,
                Cnpj = drugstore.Cnpj,
                State = drugstore.State,
                City = drugstore.City,
                Complement = drugstore.Complement,
                Cep = drugstore.Cep,
                Street = drugstore.Street,
                StreetNumber = drugstore.StreetNumber,
                Password = PasswordHasher.Hash(drugstore.Password),
                Roles = drugstore.Roles
            };
            try
            {
                await context.Drugstores.AddAsync(model);
                await context.SaveChangesAsync();
                return Created($"v1/create-drugstore/{model.Id}", model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "MFAPI5003 - Erro interno no servidor ao cadastrar usuário");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(
            [FromBody] LoginValidateModel loginValidateModel, 
            [FromServices] DataContext context, 
            [FromServices] TokenService tokenService,
            CancellationToken cancellationToken)
        {
            string roles = loginValidateModel.Roles;
            IUsers? user = null;
            if (roles == "Client")
            {
                user = await context.Clients.AsNoTracking().FirstOrDefaultAsync(x => x.Email == loginValidateModel.Email);
            }
            else if (roles == "Doctor")
            {
                user = await context.Doctors.AsNoTracking().FirstOrDefaultAsync(x => x.Email == loginValidateModel.Email);
            }
            else if (roles == "Drugstore")
            {
                user = await context.Drugstores.AsNoTracking().FirstOrDefaultAsync(x => x.Email == loginValidateModel.Email);
            }
            else
            {
                return StatusCode(401, "MFAPI4010 - Role inválido");
            }
            if (user == null)
                return StatusCode(401, "MFAPI4011 - Usuário não encontrado");

            if(!PasswordHasher.Verify(user.Password, loginValidateModel.Password))
                return StatusCode(401, "MFAPI4012 - Senha inválida");
            try
            {
                var token = tokenService.GenerateToken(user);
                return Ok(token);
            }
            catch
            {
                return StatusCode(500, "MFAPI5005 - Erro interno no servidor ao buscar cliente");
            }
        }


        [Authorize(Roles = "Client")]
        [HttpGet("client-test")]
        public IActionResult GetUser(CancellationToken cancellationToken) => Ok("Client Logado");
    }
}
