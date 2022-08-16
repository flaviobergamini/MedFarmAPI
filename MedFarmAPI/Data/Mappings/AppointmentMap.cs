using MedFarmAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedFarmAPI.Data.Mappings
{
    public class AppointmentMap : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointment");

            // Chave Primária
            builder.HasKey(x => x.Id);
            //Identity
            builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();

            //atributos da tabela

            builder.Property(x => x.DateTimeAppointment)
            .IsRequired()
            .HasColumnName("DateTimeAppointment")
            .HasColumnType("DATETIME");

            builder.Property(x => x.Remote)
            .IsRequired()
            .HasColumnName("Remote")
            .HasColumnType("BIT");

            builder.Property(x => x.VideoCallUrl)
            .IsRequired(false)
            .HasColumnName("VideoCallUrl")
            .HasColumnType("TEXT");

            builder.Property(x => x.Confirmed)
            .IsRequired()
            .HasColumnName("Confirmed")
            .HasColumnType("BIT");

            builder.Property(x => x.Payment)
            .IsRequired(true)
            .HasColumnName("Payment")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(20);

            builder.HasOne(x => x.Client).WithMany(x => x.Appointments)
                .HasConstraintName("FK_Appointments_Clients")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Doctor).WithMany(x => x.Appointments)
                .HasConstraintName("FK_Appointments_Doctor")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
