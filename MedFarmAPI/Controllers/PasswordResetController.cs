using MedFarmAPI.Data;
using MedFarmAPI.Request.PaswordResetRequest;
using MedFarmAPI.Response;
using MedFarmAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("v1/password/reset")]
    public class PasswordResetController : ControllerBase
    {
        private DecryptedTokenService? decryptedTokenService;

        [AllowAnonymous]
        [HttpPatch("user")]
        public async Task<IActionResult> PatchClientPasswordAsync(
            [FromBody] PasswordResetRequest passwordReset, 
            [FromServices] TokenService tokenService,
            [FromServices] DataContext context,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(new MessageModel
                {
                    Code = "MFAPI40012",
                    Message = "Invalid User"
                });

            try
            {
                decryptedTokenService = tokenService.ReadToken(passwordReset.Token);
                if (decryptedTokenService.Role == "Client") 
                { 
                    var user = await context.Clients.FirstOrDefaultAsync(x => x.Id == decryptedTokenService.Id);

                    if (user == null)
                    {
                        return NotFound(new MessageModel
                        {
                            Code = "MFAPI40416",
                            Message = "User not found"
                        });
                    }
                    if (passwordReset.NewPassword == passwordReset.ConfirmPassword)
                    {
                        if (!PasswordHasher.Verify(user.Password, passwordReset.NewPassword))
                        {
                            user.Password = PasswordHasher.Hash(passwordReset.NewPassword);
                            context.Clients.Update(user);
                            await context.SaveChangesAsync();
                            return Ok(new MessageModel
                            {
                                Code = "MFAPI20015",
                                Message = "Password updated successfully"
                            });
                        }
                        else
                        {
                            return BadRequest(new MessageModel
                            {
                                Code = "MFAPI40010",
                                Message = "New password cannot be the same as the old one"
                            });
                        }
                    }
                    else
                    {
                        return BadRequest(new MessageModel
                        {
                            Code = "MFAPI40011",
                            Message = "Passwords are not the same"
                        });
                    }
                }
                else if (decryptedTokenService.Role == "Doctor")
                {
                    var user = await context.Doctors.FirstOrDefaultAsync(x => x.Id == decryptedTokenService.Id);

                    if (user == null)
                    {
                        return NotFound(new MessageModel
                        {
                            Code = "MFAPI40417",
                            Message = "User not found"
                        });
                    }
                    if (passwordReset.NewPassword == passwordReset.ConfirmPassword)
                    {
                        if (!PasswordHasher.Verify(user.Password, passwordReset.NewPassword))
                        {
                            user.Password = PasswordHasher.Hash(passwordReset.NewPassword);
                            context.Doctors.Update(user);
                            await context.SaveChangesAsync();
                            return Ok(new MessageModel
                            {
                                Code = "MFAPI20016",
                                Message = "Password updated successfully"
                            });
                        }
                        else
                        {
                            return BadRequest(new MessageModel
                            {
                                Code = "MFAPI40011",
                                Message = "New password cannot be the same as the old one"
                            });
                        }
                    }
                    else
                    {
                        return BadRequest(new MessageModel
                        {
                            Code = "MFAPI40012",
                            Message = "Passwords are not the same"
                        });
                    }
                }
                else if (decryptedTokenService.Role == "Drugstore")
                {
                    var user = await context.Drugstores.FirstOrDefaultAsync(x => x.Id == decryptedTokenService.Id);

                    if (user == null)
                    {
                        return NotFound(new MessageModel
                        {
                            Code = "MFAPI40418",
                            Message = "User not found"
                        });
                    }
                    if (passwordReset.NewPassword == passwordReset.ConfirmPassword)
                    {
                        if (!PasswordHasher.Verify(user.Password, passwordReset.NewPassword))
                        {
                            user.Password = PasswordHasher.Hash(passwordReset.NewPassword);
                            context.Drugstores.Update(user);
                            await context.SaveChangesAsync();
                            return Ok(new MessageModel
                            {
                                Code = "MFAPI20017",
                                Message = "Password updated successfully"
                            });
                        }
                        else
                        {
                            return BadRequest(new MessageModel
                            {
                                Code = "MFAPI40012",
                                Message = "New password cannot be the same as the old one"
                            });
                        }
                    }
                    else
                    {
                        return BadRequest(new MessageModel
                        {
                            Code = "MFAPI40013",
                            Message = "Passwords are not the same"
                        });
                    }
                }
                else
                {
                    return NotFound(new MessageModel
                    {
                        Code = "MFAPI40419",
                        Message = "User not found"
                    });
                }
            }
            catch
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI50020",
                    Message = "Internal server error when update a client password"
                });
            }
            
        }

        [AllowAnonymous]
        [HttpPost("forgot")]
        public async Task<IActionResult> PostForgotPasswordAsync(
            [FromBody] PasswordForgotRequest passwordForgotRequest,
            [FromServices] TokenService tokenService,
            [FromServices] EmailService emailService,
            [FromServices] ViewBodyService viewBodyService,
            [FromServices] DataContext context,
            CancellationToken cancellationToken)
        {
            string email = passwordForgotRequest.Email;
            Models.Client? client = await context.Clients.FirstOrDefaultAsync(x => x.Email == email, cancellationToken: cancellationToken);
            Models.Doctor? doctor = await context.Doctors.FirstOrDefaultAsync(x => x.Email == email, cancellationToken: cancellationToken);
            Models.Drugstore? drugstore = await context.Drugstores.FirstOrDefaultAsync(x => x.Email == email, cancellationToken: cancellationToken);

            if(client != null)
            {
                var tokenClient = tokenService.GenerateClientToken(client);

                try 
                {
                    var sendEmail = emailService.Send(
                        client.Name,
                        client.Email,
                        "MedFarm - Redefinição de Senha",
                        viewBodyService.BodyEmailPasswordReset(client.Name, tokenClient));

                    if (sendEmail)
                    {
                        return Ok(new MessageModel
                        {
                            Code = "MFAPI200",
                            Message = "Email successfully sent."
                        });
                    }
                    else
                    {
                        return NotFound(new MessageModel
                        {
                            Code = "MFAPI404",
                            Message = "Invalid email"
                        });
                    }
                } catch {
                    return StatusCode(500, new MessageModel
                    {
                        Code = "MFAPI500",
                        Message = "Mail email error or invalid email"
                    });
                }
            }
            else if (doctor != null)
            {
                var tokenDoctor = tokenService.GenerateDoctorToken(doctor);

                try
                {
                    var sendEmail = emailService.Send(
                        doctor.Name,
                        doctor.Email,
                        "MedFarm - Redefinição de Senha",
                        viewBodyService.BodyEmailPasswordReset(doctor.Name, tokenDoctor));

                    if (sendEmail)
                    {
                        return Ok(new MessageModel
                        {
                            Code = "MFAPI200",
                            Message = "Email successfully sent."
                        });
                    }
                    else
                    {
                        return NotFound(new MessageModel
                        {
                            Code = "MFAPI404",
                            Message = "Invalid email"
                        });
                    }
                }
                catch
                {
                    return StatusCode(500, new MessageModel
                    {
                        Code = "MFAPI500",
                        Message = "Mail email error or invalid email"
                    });
                }
            }
            if (drugstore != null)
            {
                var tokenClient = tokenService.GenerateDrugstoreToken(drugstore);

                try
                {
                    var sendEmail = emailService.Send(
                        drugstore.Name,
                        drugstore.Email,
                        "MedFarm - Redefinição de Senha",
                        viewBodyService.BodyEmailPasswordReset(drugstore.Name, tokenClient));

                    if (sendEmail)
                    {
                        return Ok(new MessageModel
                        {
                            Code = "MFAPI200",
                            Message = "Email successfully sent."
                        });
                    }
                    else
                    {
                        return NotFound(new MessageModel
                        {
                            Code = "MFAPI404",
                            Message = "Invalid email"
                        });
                    }
                }
                catch
                {
                    return StatusCode(500, new MessageModel
                    {
                        Code = "MFAPI500",
                        Message = "Mail email error or invalid email"
                    });
                }
            }
            else
            {
                return NotFound(new MessageModel
                {
                    Code = "MFAPI404",
                    Message = "Invalid email"
                });
            }
        }
    }
}
