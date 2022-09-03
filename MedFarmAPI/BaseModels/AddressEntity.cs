using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.BaseModels
{
    public abstract class AddressEntity
    {
        [Required] public string State { get; set; } = null!;
        [Required] public string City { get; set; } = null!;
        public string? Complement { get; set; } = string.Empty;
        [Required] public string District { get; set; } = null!;
        [Required] public string Cep { get; set; } = null!;
        [Required] public string Street { get; set; } = null!;
        [Required] public int StreetNumber { get; set; }
    }
}
