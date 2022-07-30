﻿using MedFarmAPI.Entities;
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
            .IsRequired()
            .HasColumnName("VideoCallUrl")
            .HasColumnType("TEXT");

            builder.HasOne(x => x.Client).WithMany(x => x.Appointments)
                .HasConstraintName("FK_Appointments_Clients")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Doctor).WithMany(x => x.Appointments)
                .HasConstraintName("FK_Appointments_Doctor")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}