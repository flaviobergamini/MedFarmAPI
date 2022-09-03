namespace MedFarmAPI.MessageResponseModel
{
    public class LoginResponse
    {
        public string Code { get; set; } = null!;
        public int Id { get; set; }
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
