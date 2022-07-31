using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.BaseModels
{

    public abstract class UserEntity:AddressEntity
    {
        [Required] public int Id { get; set; }
        [Required] public string Name { get; set; } = null!;
        [Required] public string Email { get; set; } = null!;
        [Required] public string Phone { get; set; } = null!;
    }
}