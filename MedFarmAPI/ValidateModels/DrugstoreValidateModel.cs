using MedFarmAPI.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.ValidateModels
{
    public class DrugstoreValidateModel:UserEntity
    {
        [Required] public string Cnpj { get; set; } = null!;
    }
}
