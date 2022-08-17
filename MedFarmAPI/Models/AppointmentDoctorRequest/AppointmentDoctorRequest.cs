using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.Models.AppointmentDoctorRequest
{
    public class AppointmentDoctorRequest
    {
        [Required] public int Id { get; set; }
        [Required] public DateTime Date { get; set; }
    }
}
