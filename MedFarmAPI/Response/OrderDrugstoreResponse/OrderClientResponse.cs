using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.Response.OrderDrugstoreResponse
{
    public class OrderClientResponse
    {
        [Required] public int Id { get; set; }
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Street { get; set; } = string.Empty;
        [Required] public string District { get; set; } = string.Empty;
        [Required] public string City { get; set; } = string.Empty;
        [Required] public int StreetNumber { get; set; }
        public string? Image { get; set; } = string.Empty;
    }
}
