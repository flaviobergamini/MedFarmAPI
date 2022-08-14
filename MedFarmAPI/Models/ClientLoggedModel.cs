namespace MedFarmAPI.Models
{
    public class ClientLoggedModel
    {
        public int Id { get; set; }
        public string RefreshToken { get; set; } = null!;
    }
}
