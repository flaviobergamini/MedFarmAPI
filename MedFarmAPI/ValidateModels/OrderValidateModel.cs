﻿using MedFarmAPI.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.ValidateModels
{
    public class OrderValidateModel:AddressEntity
    {
        [Required] public int Id { get; set; }
        [Required] public string Image { get; set; } = null!;
    }
}
