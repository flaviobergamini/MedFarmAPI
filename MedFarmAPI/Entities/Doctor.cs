using MedFarmAPI.Model;

namespace MedFarmAPI.Entities
{
    public class Doctor:UserEntity
    {
        public string Cpf { get; set; } = string.Empty!;
        public string Specialty { get; set; } = string.Empty!;
        public string RegionalCouncil { get; set; } = string.Empty!;
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}