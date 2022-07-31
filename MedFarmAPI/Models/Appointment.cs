using System.ComponentModel.DataAnnotations;

namespace MedFarmAPI.Models
{
    public class Appointment{
        [Required] public int Id { get; set; }
        [Required] public DateTime DateTimeAppointment { get; set; }
        [Required] public bool Remote { get; set; }
        public string? VideoCallUrl { get; set; } = string.Empty;
        [Required] public Client Client { get; set; } = new Client();
        [Required] public Doctor Doctor { get; set; } = new Doctor();

    }
}