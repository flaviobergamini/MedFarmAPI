namespace MedFarmAPI.Model
{
    public abstract class AddressEntity
    {
        public string State { get; set; } = string.Empty!;
        public string City { get; set; } = string.Empty!;
        public string? Complement { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty!;
        public string Street { get; set; } = string.Empty!;
        public int StreetNumber { get; set; }
    }
}
