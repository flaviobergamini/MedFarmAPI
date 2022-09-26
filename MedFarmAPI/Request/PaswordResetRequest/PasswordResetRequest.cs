using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.Request.PaswordResetRequest
{
    public class PasswordResetRequest
    {
        [Required]
        public string Token { get; set; } = null!;

        [Required]
        public string NewPassword { get; set; } = null!;

        [Required]
        public string ConfirmPassword { get; set; } = null!;
    }
}
