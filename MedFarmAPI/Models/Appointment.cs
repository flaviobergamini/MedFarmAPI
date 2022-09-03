namespace MedFarmAPI.Models
{
    public class Appointment{
        public int Id { get; set; }
        public DateTime DateTimeAppointment { get; set; }
        public bool Remote { get; set; }
        public string? VideoCallUrl { get; set; } = string.Empty;
        public bool Confirmed { get; set; }
        public string Payment { get; set; } = string.Empty;
        public Client Client { get; set; } = new Client();
        public Doctor Doctor { get; set; } = new Doctor();

    }
}