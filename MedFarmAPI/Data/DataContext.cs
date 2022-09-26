using MedFarmAPI.Data.Mappings;
using MedFarmAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MedFarmAPI.Data{
    public class DataContext:DbContext
    {
        public DbSet<Client>? Clients { get; set; }
        public DbSet<Doctor>? Doctors { get; set; }
        public DbSet<Drugstore>? Drugstores { get; set; }
        public DbSet<Appointment>? Appointments { get; set; }
        public DbSet<Order>? Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=tcp:medfarm-server.database.windows.net,1433;Initial Catalog=MedFarmDatabase;Persist Security Info=False;User ID=medfarmadministrator;Password=Abelha@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            //options.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.ApplyConfiguration(new ClientMap());
            modelBuilder.ApplyConfiguration(new DoctorMap());
            modelBuilder.ApplyConfiguration(new DrugstoreMap());
            modelBuilder.ApplyConfiguration(new AppointmentMap());
            modelBuilder.ApplyConfiguration(new OrderMap());
        }
    }
}