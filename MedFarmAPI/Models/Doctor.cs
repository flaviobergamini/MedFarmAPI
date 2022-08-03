using MedFarmAPI.BaseModels;
using MedFarmAPI.Interfaces;

namespace MedFarmAPI.Models
{
    public class Doctor:UserEntity
    {
        public string Cpf { get; set; } = null!;
        public string Specialty { get; set; } = null!;
        public string RegionalCouncil { get; set; } = null!;
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}