﻿using MedFarmAPI.Data;
using MedFarmAPI.MessageResponseModel;
using MedFarmAPI.Models;
using MedFarmAPI.ValidateModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedFarmAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class OrderController:ControllerBase
    {
        [HttpGet("order")]
        public async Task<IActionResult> GetAsync([FromServices] DataContext context)
        {
            try
            {
                var orders = await context.Orders.AsNoTracking().ToListAsync();

                if (orders == null)
                    return NotFound();

                return Ok(orders);
            }
            catch
            {
                return StatusCode(500, "MFAPI5002 - Erro interno no servidor ao buscar cliente");
            }
        }
    }
}
