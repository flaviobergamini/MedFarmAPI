using MedFarmAPI.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.Models
{
    public class Drugstore : UserEntity
    {
        [Required] public string Cnpj { get; set; } = null!;
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}