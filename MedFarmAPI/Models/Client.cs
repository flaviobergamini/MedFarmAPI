using MedFarmAPI.BaseModels;
using MedFarmAPI.Interfaces;

namespace MedFarmAPI.Models
{
    public class Client:UserEntity
    {
        public string Cpf { get; set; } = null!;
        public IList<Appointment> Appointments { get; set; } = new List<Appointment>();
        public IList<Order> Orders { get; set; } = new List<Order>();
    }
}