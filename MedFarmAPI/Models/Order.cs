using MedFarmAPI.BaseModels;

namespace MedFarmAPI.Models
{
    public class Order:AddressEntity
    {
        public int Id { get; set; }
        public string Image { get; set; } = null!;
        public DateTime DateTimeOrder { get; set; }
        public Client Client { get; set; } = new Client();
        public Drugstore Drugstores { get; set; } = new Drugstore();
    }
}
