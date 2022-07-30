namespace MedFarmAPI.Entities
{
    public class Appointment{
        public int id { get; set; }
        public DateTime DateTime { get; set; }
        public bool Remote { get; set; }
        public string? VideoCallUrl { get; set; } = string.Empty;
        public Client Client { get; set; } = new Client();
        public Doctor Doctor { get; set; } = new Doctor();

    }
}