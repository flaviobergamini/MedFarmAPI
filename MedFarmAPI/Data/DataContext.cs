using MedFarmAPI.Data.Mappings;
using MedFarmAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MedFarmAPI.Data{
    public class DataContext:DbContext
    {
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Drugstore> Drugstores => Set<Drugstore>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Order> Orders => Set<Order>();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=localhost,1433;Database=MedFarm;User ID=sa;Password=/MS-DOSV.6.22b");
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