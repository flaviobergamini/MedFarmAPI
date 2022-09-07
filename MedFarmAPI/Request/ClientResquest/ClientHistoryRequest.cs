namespace MedFarmAPI.Request.ClientResquest
{
    public class ClientHistoryRequest
    {
        public int Id { get; set; }
        public string RefreshToken { get; set; } = null!;
    }
}
