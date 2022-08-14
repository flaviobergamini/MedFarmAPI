using MedFarmAPI.MessageResponseModel.ClientResponse.Model;

namespace MedFarmAPI.MessageResponseModel.ClientResponse
{
    public class ClientLoggedOrderResponse : ClientLogged
    {
        public DateTime DrugstoreDateTimeOrder { get; set; }

    }
}
