using MedFarmAPI.BaseModels;

namespace MedFarmAPI.ValidateModels
{
    public class OrderValidateModel:AddressEntity
    {
        [Required] public int Id { get; set; }
        [Required] public string Image { get; set; } = null!;
    }
}
