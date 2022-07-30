using MedFarmAPI.Model;

namespace MedFarmAPI.Entities
{
    public class Order:AddressEntity
    {
        public int Id { get; set; }
        public string Image { get; set; } = string.Empty!;
        public Client Client { get; set; } = new Client();
        public Drugstore Drugstores { get; set; } = new Drugstore();
    }
}
