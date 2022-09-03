using MedFarmAPI.MessageResponseModel.ClientResponse.Model;

namespace MedFarmAPI.MessageResponseModel.ClientResponse
{
    public class ClientLoggedDoctorResponse : ClientLogged
    {
        public string DoctorSpecialty { get; set; } = string.Empty;
        public DateTime DoctorDateTimeAppointment { get; set; }
    }
}
