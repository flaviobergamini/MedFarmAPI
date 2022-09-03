namespace MedFarmAPI.BaseModels
{
    public abstract class UserEntity:AddressEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public string Roles { get; set; } = null!;
    }
}