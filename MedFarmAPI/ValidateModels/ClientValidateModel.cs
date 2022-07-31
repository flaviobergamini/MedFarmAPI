using MedFarmAPI.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.ValidateModels
{
    public class ClientValidateModel: UserEntity
    {
        [Required] public string Cpf { get; set; } = null!;
    }
}
