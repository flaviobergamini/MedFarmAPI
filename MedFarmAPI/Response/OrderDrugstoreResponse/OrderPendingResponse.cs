using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.Response.OrderDrugstoreResponse
{
    public class OrderPendingResponse
    {
        [Required] public int Id { get; set; }
        [Required] public string Name { get; set; } = string.Empty;
    }
}
