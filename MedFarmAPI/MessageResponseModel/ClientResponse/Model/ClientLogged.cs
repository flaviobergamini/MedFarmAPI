using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.MessageResponseModel.ClientResponse.Model
{
    public abstract class ClientLogged
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public string Street { get; set; } = string.Empty;
    }
}
