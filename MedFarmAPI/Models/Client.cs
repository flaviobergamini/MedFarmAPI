using MedFarmAPI.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.Models
{
    public class Client:UserEntity
    {
        [Required] public string Cpf { get; set; } = null!;
        public IList<Appointment> Appointments { get; set; } = new List<Appointment>();
        public IList<Order> Orders { get; set; } = new List<Order>();
    }
}