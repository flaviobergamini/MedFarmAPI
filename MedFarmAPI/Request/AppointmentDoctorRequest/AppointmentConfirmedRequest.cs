using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.Request.AppointmentDoctorRequest
{
    public class AppointmentConfirmedRequest
    {
        [Required] public int Id { get; set; }
        [Required] public DateTime Date { get; set; }
    }
}
