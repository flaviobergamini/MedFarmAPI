using MedFarmAPI.BaseModels;
using MedFarmAPI.Interfaces;

namespace MedFarmAPI.Models
{
    public class Drugstore : UserEntity
    {
        public string Cnpj { get; set; } = null!;
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}