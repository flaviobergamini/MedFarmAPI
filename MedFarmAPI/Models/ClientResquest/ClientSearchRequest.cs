using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.Models.ClientResquest
{
    public class ClientSearchRequest
    {
        [Required] public string Category { get; set; } = string.Empty; // doctor or drugstore
        public string? Specialty { get; set; } = string.Empty;
        [Required] public string City { get; set; } = string.Empty;
    }
}
