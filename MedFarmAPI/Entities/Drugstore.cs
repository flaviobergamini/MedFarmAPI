using MedFarmAPI.Model;

namespace MedFarmAPI.Entities
{
    public class Drugstore : UserEntity
    {
        public string Cnpj { get; set; } = string.Empty!;
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}