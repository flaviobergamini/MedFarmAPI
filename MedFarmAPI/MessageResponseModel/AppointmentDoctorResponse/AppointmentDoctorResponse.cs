﻿using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.MessageResponseModel.AppointmentDoctorResponse
{
    public class AppointmentDoctorResponse
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public DateTime Date { get; set; }
        [Required] public bool Remote { get; set; }
    }
}
