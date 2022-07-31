using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.ValidateModels
{
    public class AppointmentValidateModel
    {
        [Required] public int Id { get; set; }
        [Required] public DateTime DateTimeAppointment { get; set; }
        [Required] public bool Remote { get; set; }
        public string? VideoCallUrl { get; set; } = string.Empty;
    }
}
