using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.Response.AppointmentDoctorResponse
{
    public class AppointmentConfirmedResponse
    {
        [Required] public string Name { get; set; } = string.Empty;
        [Required] public DateTime Date { get; set; }
        [Required] public bool Remote { get; set; }
    }
}
