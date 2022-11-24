namespace MedFarmAPI.Response.ClientResponse
{
    public class ClientModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Cpf { get; set; } = null!;
        public string State { get; set; } = null!;
        public string City { get; set; } = null!;
        public string? Complement { get; set; } = string.Empty;
        public string District { get; set; } = null!;
        public string Cep { get; set; } = null!;
        public string Street { get; set; } = null!;
        public int StreetNumber { get; set; }
    }
}
