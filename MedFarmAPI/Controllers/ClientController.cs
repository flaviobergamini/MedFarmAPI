﻿using MedFarmAPI.Data;
using MedFarmAPI.Models;
using MedFarmAPI.ValidateModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ClientController : ControllerBase
    {
        [HttpPost("create-client")]
        public async Task<IActionResult> PostAsync([FromBody] ClientValidateModel client, [FromServices] DataContext context)
        {
            if(!ModelState.IsValid)
                return BadRequest("MFAPI4000 - Cliente inválido");

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
                StreetNumber = client.StreetNumber
            };
            try
            {
                await context.Clients.AddAsync(model);
                await context.SaveChangesAsync();
                return Created($"v1/create-client/{model.Id}", model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "MFAPI5001 - Erro interno no servidor ao cadastrar cliente");
            }
        }

        [HttpGet("clients")]
        public async Task<IActionResult> GetAsync([FromServices] DataContext context)
        {
            try
            {
                var clients = await context.Clients.ToListAsync();

                if (clients == null)
                    return NotFound();

                return Ok(clients);
            }
            catch
            {
                return StatusCode(500, "MFAPI5002 - Erro interno no servidor ao buscar cliente");
            }
        }
    }
}
