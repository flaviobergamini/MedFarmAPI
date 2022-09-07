namespace MedFarmAPI.Response
{
    public class SignUpResponse
    {
        public string Code { get; set; } = null!;
        public int Id { get; set; }
        public string RefreshToken { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
