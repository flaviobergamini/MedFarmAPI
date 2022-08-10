namespace MedFarmAPI.MessageResponseModel
{
    public class OrderResponse
    {
        public string Code { get; set; } = null!;
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int DrugstoreId { get; set; }
    }
}
