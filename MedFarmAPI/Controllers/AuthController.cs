using MedFarmAPI.Data;
using MedFarmAPI.Response;
using MedFarmAPI.Models;
using MedFarmAPI.Services;
using MedFarmAPI.ValidateModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using DocumentValidation;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("v1/auth")]
    public class AuthController:ControllerBase
    {
        [HttpPost("create/client")]
        public async Task<IActionResult> PostClientAsync(
            [FromBody] ClientValidateModel client,
            [FromServices] EmailService emailService,
            [FromServices] ViewBodyService viewBodyService,
            [FromServices] DataContext context,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid || !client.Cpf.ValidateCpf() || !client.Email.ValidateEmail() ||
                !client.Phone.ValidatePhone() || !client.Cep.ValidateCep())
                return BadRequest(new MessageModel 
                {
                    Code = "MFAPI4001",
                    Message = "Invalid User"
                });

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
                District = client.District,
                Password = PasswordHasher.Hash(client.Password),
                RefreshToken = Convert.ToBase64String((Guid.NewGuid()).ToByteArray()),
            Roles = client.Roles
            };
            try
            {
                var confirmEmail = emailService.Send(
                    model.Name,
                    model.Email,
                    "Bem vindo ao Med Farm!!!",
                    viewBodyService.BodyEmail(model.Name)
                    ); 
               
                if (confirmEmail)
                {
                    await context.Clients.AddAsync(model);
                    await context.SaveChangesAsync();
                    return StatusCode(201, new SignUpResponse
                    {
                        Code = "MFAPI2010",
                        Id = model.Id,
                        Name = model.Name,
                        RefreshToken = model.RefreshToken
                    });
                }
                else
                {
                    return NotFound(new MessageModel
                    {
                        Code = "MFAPI4042",
                        Message = "Invalid email"
                    });
                }
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI5001",
                    Message = "Database error"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI5002",
                    Message = "Internal server error when registering user: " + ex.Message
                }) ;
            }
        }

        [HttpPost("create/doctor")]
        public async Task<IActionResult> PostDoctorAsync(
            [FromBody] DoctorValidateModel doctor,
            [FromServices] EmailService emailService,
            [FromServices] ViewBodyService viewBodyService,
            [FromServices] DataContext context,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid || !doctor.Cpf.ValidateCpf() || !doctor.Email.ValidateEmail() ||
                !doctor.Phone.ValidatePhone() || !doctor.Cep.ValidateCep())
                    return BadRequest(new MessageModel
                {
                    Code = "MFAPI4002",
                    Message = "Invalid User"
                });

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
                District = doctor.District,
                Password = PasswordHasher.Hash(doctor.Password),
                RefreshToken = Convert.ToBase64String((Guid.NewGuid()).ToByteArray()),
                Roles = doctor.Roles,
                Specialty = doctor.Specialty,
                RegionalCouncil = doctor.RegionalCouncil
            };

            try
            {
                var confirmEmail = emailService.Send(
                    model.Name,
                    model.Email,
                    "Bem vindo ao Med Farm!!!",
                    viewBodyService.BodyEmail(model.Name)
                    );

                if (confirmEmail)
                {
                    await context.Doctors.AddAsync(model);
                    await context.SaveChangesAsync();
                    return StatusCode(201, new SignUpResponse
                    {
                        Code = "MFAPI2011",
                        Id = model.Id,
                        Name = model.Name,
                        RefreshToken = model.RefreshToken
                    });
                }
                else
                {
                    return NotFound(new MessageModel
                    {
                        Code = "MFAPI4043",
                        Message = "Invalid email"
                    });
                }
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI5003",
                    Message = "Database error"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI5004",
                    Message = "Internal server error when registering user"
                });
            }
        }

        [HttpPost("create/drugstore")]
        public async Task<IActionResult> PostDrugstoreAsync(
            [FromBody] DrugstoreValidateModel drugstore,
            [FromServices] EmailService emailService,
            [FromServices] ViewBodyService viewBodyService,
            [FromServices] DataContext context, 
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid || !drugstore.Cnpj.ValidateCnpj() || !drugstore.Email.ValidateEmail() ||
                !drugstore.Phone.ValidatePhone() || !drugstore.Cep.ValidateCep())
                return BadRequest(new MessageModel
                {
                    Code = "MFAPI4003",
                    Message = "Invalid User"
                });

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
                District = drugstore.District,
                Password = PasswordHasher.Hash(drugstore.Password),
                RefreshToken = Convert.ToBase64String((Guid.NewGuid()).ToByteArray()),
                Roles = drugstore.Roles
            };
            try
            {
                var confirmEmail = emailService.Send(
                    model.Name,
                    model.Email,
                    "Bem vindo ao Med Farm!!!",
                    viewBodyService.BodyEmail(model.Name)
                    );

                if (confirmEmail)
                {
                    await context.Drugstores.AddAsync(model);
                    await context.SaveChangesAsync();
                    return StatusCode(201, new SignUpResponse
                    {
                        Code = "MFAPI2012",
                        Id = model.Id,
                        Name = model.Name,
                        RefreshToken = model.RefreshToken
                    });
                }
                else
                {
                    return NotFound(new MessageModel
                    {
                        Code = "MFAPI4044",
                        Message = "Invalid email"
                    });
                }
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI5005",
                    Message = "Database error"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI5006",
                    Message = "Internal server error when registering user"
                });
            }
        }


        [HttpPost("login/client")]
        public async Task<IActionResult> LoginClientAsync(
            [FromBody] LoginValidateModel loginValidateModel, 
            [FromServices] DataContext context, 
            [FromServices] TokenService tokenService,
            CancellationToken cancellationToken)
        {
 
            Client? user = await context.Clients.AsNoTracking().FirstOrDefaultAsync(x => x.Email == loginValidateModel.Email);
            
            if (user == null)
                return NotFound(new MessageModel
                {
                    Code = "MFAPI4045",
                    Message = "User not found"
                });

           if(!PasswordHasher.Verify(user.Password, loginValidateModel.Password))
                return StatusCode(401, new MessageModel
                {
                    Code = "MFAPI4010",
                    Message = "Invalid password"
                });
            try
            {
                var token = tokenService.GenerateClientToken(user);
                return Ok(new LoginResponse
                {
                    Code = "MFAPI2003",
                    AccessToken = token,
                    RefreshToken = user.RefreshToken,
                    Id = user.Id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI5007",
                    Message = "Internal server error when fetching user"
                });
            }
        }

        [HttpPost("login/doctor")]
        public async Task<IActionResult> LoginDoctorAsync(
            [FromBody] LoginValidateModel loginValidateModel,
            [FromServices] DataContext context,
            [FromServices] TokenService tokenService,
            CancellationToken cancellationToken)
        {

            Doctor? user = await context.Doctors.AsNoTracking().FirstOrDefaultAsync(x => x.Email == loginValidateModel.Email);

            if (user == null)
                return NotFound(new MessageModel
                {
                    Code = "MFAPI4046",
                    Message = "User not found"
                });

            if (!PasswordHasher.Verify(user.Password, loginValidateModel.Password))
                return StatusCode(401, new MessageModel
                {
                    Code = "MFAPI4011",
                    Message = "Invalid password"
                });
            try
            {
                var token = tokenService.GenerateDoctorToken(user);
                return Ok(new LoginResponse
                {
                    Code = "MFAPI20010",
                    AccessToken = token,
                    RefreshToken = user.RefreshToken,
                    Id = user.Id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI5008",
                    Message = "Internal server error when fetching user"
                });
            }
        }

        [HttpPost("login/drugstore")]
        public async Task<IActionResult> LoginDrugstoreAsync(
            [FromBody] LoginValidateModel loginValidateModel,
            [FromServices] DataContext context,
            [FromServices] TokenService tokenService,
            CancellationToken cancellationToken)
        {

            Drugstore? user = await context.Drugstores.AsNoTracking().FirstOrDefaultAsync(x => x.Email == loginValidateModel.Email);

            if (user == null)
                return NotFound(new MessageModel
                {
                    Code = "MFAPI4047",
                    Message = "User not found"
                });

            if (!PasswordHasher.Verify(user.Password, loginValidateModel.Password))
                return StatusCode(401, new MessageModel
                {
                    Code = "MFAPI4012",
                    Message = "Invalid password"
                });
            try
            {
                var token = tokenService.GenerateDrugstoreToken(user);
                return Ok(new LoginResponse
                {
                    Code = "MFAPI20011",
                    AccessToken = token,
                    RefreshToken = user.RefreshToken,
                    Id = user.Id
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI5009",
                    Message = "Internal server error when fetching user"
                });
            }
        }
    }
}
