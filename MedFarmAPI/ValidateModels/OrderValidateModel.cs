using MedFarmAPI.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.ValidateModels
{
    public class OrderValidateModel:AddressEntity
    {
        [Required] public string Image { get; set; } = null!;
        [Required] public int ClientId { get; set; }
        [Required] public int DrugstoresId { get; set; }
        [Required] public DateTime DateTimeOrder { get; set; }
    }
}
