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
        //[Authorize(Roles = "Client")]
        //[EnableCors]
        [AllowAnonymous]
        [HttpPatch("client")]
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
                var id = int.Parse(tokenService.ReadToken(passwordReset.Token));
                var user = await context.Clients.FirstOrDefaultAsync(x => x.Id == id);

                if(user == null)
                {
                    return NotFound(new MessageModel
                    {
                        Code = "MFAPI40416",
                        Message = "Client not found"
                    });
                }
                if (passwordReset.NewPassword == passwordReset.ConfirmPassword)
                {
                    if(!PasswordHasher.Verify(user.Password, passwordReset.NewPassword))
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
            catch
            {
                return StatusCode(500, new MessageModel
                {
                    Code = "MFAPI50020",
                    Message = "Internal server error when update a client password"
                });
            }
            
        }
    }
}
