using MedFarmAPI.Response.ClientResponse.Model;

namespace MedFarmAPI.Response.ClientResponse
{
    public class ClientLoggedDoctorResponse : ClientLogged
    {
        public string DoctorSpecialty { get; set; } = string.Empty;
        public DateTime DoctorDateTimeAppointment { get; set; }
    }
}
