using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.MessageResponseModel.AppointmentDoctorResponse
{
    public class AppointmentPendingResponse
    {
        [Required] public int Id { get; set; }
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public DateTime Date { get; set; }
    }
}
