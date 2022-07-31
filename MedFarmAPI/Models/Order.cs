using MedFarmAPI.BaseModels;
using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.Models
{
    public class Order:AddressEntity
    {
        [Required] public int Id { get; set; }
        [Required] public string Image { get; set; } = null!;
        [Required] public Client Client { get; set; } = new Client();
        [Required] public Drugstore Drugstores { get; set; } = new Drugstore();
    }
}
