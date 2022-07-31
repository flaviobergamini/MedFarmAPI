using MedFarmAPI.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.ValidateModels
{
    public class DoctorValidateModel:UserEntity
    {
        [Required] public string Cpf { get; set; } = null!;
        [Required] public string Specialty { get; set; } = null!;
        [Required] public string RegionalCouncil { get; set; } = null!;
    }
}
