using MedFarmAPI.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.Models
{
    public class Doctor:UserEntity
    {
        [Required] public string Cpf { get; set; } = null!;
        [Required] public string Specialty { get; set; } = null!;
        [Required] public string RegionalCouncil { get; set; } = null!;
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}