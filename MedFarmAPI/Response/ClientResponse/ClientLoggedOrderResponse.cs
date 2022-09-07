using MedFarmAPI.Response.ClientResponse.Model;

namespace MedFarmAPI.Response.ClientResponse
{
    public class ClientLoggedOrderResponse : ClientLogged
    {
        public DateTime DrugstoreDateTimeOrder { get; set; }

    }
}
