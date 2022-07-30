using MedFarmAPI.Model;

namespace MedFarmAPI.Entities
{
    public class Client:UserEntity
    {
        public string Cpf { get; set; } = string.Empty!;
        public IList<Appointment> Appointments { get; set; } = new List<Appointment>();
        public IList<Order> Orders { get; set; } = new List<Order>();
    }
}