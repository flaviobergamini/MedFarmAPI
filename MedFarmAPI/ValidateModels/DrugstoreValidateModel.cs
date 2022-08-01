using MedFarmAPI.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.ValidateModels
{
    public class DrugstoreValidateModel: AddressEntity
    {
        [Required] public string Name { get; set; } = null!;
        [Required] public string Email { get; set; } = null!;
        [Required] public string Phone { get; set; } = null!;
        [Required] public string Cnpj { get; set; } = null!;
    }
}
