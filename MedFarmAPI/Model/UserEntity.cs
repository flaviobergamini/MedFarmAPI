namespace MedFarmAPI.Model
{

    public abstract class UserEntity:AddressEntity
    {
        public int id { get; set; }
        public string Name { get; set; } = string.Empty!;
        public string Email { get; set; } = string.Empty!;
        public string Phone { get; set; } = string.Empty!;
    }
}