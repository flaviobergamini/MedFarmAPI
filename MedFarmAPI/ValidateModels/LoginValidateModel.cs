using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.ValidateModels
{
    public class LoginValidateModel
    {
        [Required] public string Email { get; set; } = null!;
        [Required] public string Password { get; set; } = null!;
        [Required] public string Roles { get; set; } = null!;
    }
}
