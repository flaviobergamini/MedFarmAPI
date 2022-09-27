namespace MedFarmAPI
{
    public static class Configuration
    {
        //Usando GUID e encriptado em base64
        public static string JwtKey { get; set; } = "NzQ5MjhlMTUtYjkwOS00M2MzLWIyYmUtMzM3NmVjNjY4YWFh";
        public static SmtpConfiguration Smtp = new();
        public static FirebaseConfiguration Firebase = new();
        public static string? ConnectionStringDatabase { get; set; }

        public class SmtpConfiguration
        {
            public string Host { get; set; } = null!;
            public int Port { get; set; }
            public string UserName { get; set; } = null!;
            public string Password { get; set; } = null!;
        }

        public class FirebaseConfiguration
        {
            public string ApiKey { get; set; } = null!;
            public string Bucket { get; set; } = null!;
            public string AuthEmail { get; set; } = null!;
            public string AuthPassword { get; set; } = null!;
        }
    }
}
