using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.Response.AppointmentDoctorResponse
{
    public class AppointmentPendingResponse
    {
        [Required] public int Id { get; set; }
        [Required] public int UserId { get; set; }
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public DateTime Date { get; set; }
    }
}
