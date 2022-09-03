namespace MedFarmAPI.MessageResponseModel
{
    public class AppointmentResponse
    {
        public string Code { get; set; } = null!;
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int DoctorId { get; set; }
    }
}
