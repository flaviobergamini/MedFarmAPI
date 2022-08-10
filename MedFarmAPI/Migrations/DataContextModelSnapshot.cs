﻿// <auto-generated />
using System;
using MedFarmAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MedFarmAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MedFarmAPI.Models.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTimeAppointment")
                        .HasColumnType("DATETIME")
                        .HasColumnName("DateTimeAppointment");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<bool>("Remote")
                        .HasColumnType("BIT")
                        .HasColumnName("Remote");

                    b.Property<string>("VideoCallUrl")
                        .HasColumnType("TEXT")
                        .HasColumnName("VideoCallUrl");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("DoctorId");

                    b.ToTable("Appointment", (string)null);
                });

            modelBuilder.Entity("MedFarmAPI.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("NVARCHAR(9)")
                        .HasColumnName("Cep");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("NVARCHAR(45)")
                        .HasColumnName("City");

                    b.Property<string>("Complement")
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR(300)")
                        .HasColumnName("Complement");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("NVARCHAR(14)")
                        .HasColumnName("Cpf");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR(200)")
                        .HasColumnName("Email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR(80)")
                        .HasColumnName("Name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("NVARCHAR(1000)")
                        .HasColumnName("PasswordHash");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("NVARCHAR(14)")
                        .HasColumnName("Phone");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("NVARCHAR(1000)")
                        .HasColumnName("RefreshToken");

                    b.Property<string>("Roles")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR(20)")
                        .HasColumnName("Roles");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("NVARCHAR(45)")
                        .HasColumnName("State");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR(300)")
                        .HasColumnName("Street");

                    b.Property<int>("StreetNumber")
                        .HasColumnType("INT")
                        .HasColumnName("StreetNumber");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Client", (string)null);
                });

            modelBuilder.Entity("MedFarmAPI.Models.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("NVARCHAR(9)")
                        .HasColumnName("Cep");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("NVARCHAR(45)")
                        .HasColumnName("City");

                    b.Property<string>("Complement")
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR(300)")
                        .HasColumnName("Complement");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("NVARCHAR(14)")
                        .HasColumnName("Cpf");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR(200)")
                        .HasColumnName("Email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR(80)")
                        .HasColumnName("Name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("NVARCHAR(1000)")
                        .HasColumnName("PasswordHash");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("NVARCHAR(14)")
                        .HasColumnName("Phone");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("NVARCHAR(1000)")
                        .HasColumnName("RefreshToken");

                    b.Property<string>("RegionalCouncil")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("NVARCHAR(30)")
                        .HasColumnName("RegionalCouncil");

                    b.Property<string>("Roles")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR(20)")
                        .HasColumnName("Roles");

                    b.Property<string>("Specialty")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("NVARCHAR(30)")
                        .HasColumnName("Specialty");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("NVARCHAR(45)")
                        .HasColumnName("State");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR(300)")
                        .HasColumnName("Street");

                    b.Property<int>("StreetNumber")
                        .HasColumnType("INT")
                        .HasColumnName("StreetNumber");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Doctor", (string)null);
                });

            modelBuilder.Entity("MedFarmAPI.Models.Drugstore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("NVARCHAR(9)")
                        .HasColumnName("Cep");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("NVARCHAR(45)")
                        .HasColumnName("City");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("NVARCHAR(18)")
                        .HasColumnName("Cnpj");

                    b.Property<string>("Complement")
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR(300)")
                        .HasColumnName("Complement");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR(200)")
                        .HasColumnName("Email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("NVARCHAR(80)")
                        .HasColumnName("Name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("NVARCHAR(1000)")
                        .HasColumnName("PasswordHash");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("NVARCHAR(14)")
                        .HasColumnName("Phone");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("NVARCHAR(1000)")
                        .HasColumnName("RefreshToken");

                    b.Property<string>("Roles")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("NVARCHAR(20)")
                        .HasColumnName("Roles");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("NVARCHAR(45)")
                        .HasColumnName("State");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR(300)")
                        .HasColumnName("Street");

                    b.Property<int>("StreetNumber")
                        .HasColumnType("INT")
                        .HasColumnName("StreetNumber");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Drugstore", (string)null);
                });

            modelBuilder.Entity("MedFarmAPI.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("NVARCHAR(9)")
                        .HasColumnName("Cep");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("NVARCHAR(45)")
                        .HasColumnName("City");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Complement")
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR(300)")
                        .HasColumnName("Complement");

                    b.Property<int>("DrugstoresId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Image");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("NVARCHAR(45)")
                        .HasColumnName("State");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("NVARCHAR(300)")
                        .HasColumnName("Street");

                    b.Property<int>("StreetNumber")
                        .HasColumnType("INT")
                        .HasColumnName("StreetNumber");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("DrugstoresId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("MedFarmAPI.Models.Appointment", b =>
                {
                    b.HasOne("MedFarmAPI.Models.Client", "Client")
                        .WithMany("Appointments")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Appointments_Clients");

                    b.HasOne("MedFarmAPI.Models.Doctor", "Doctor")
                        .WithMany("Appointments")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Appointments_Doctor");

                    b.Navigation("Client");

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("MedFarmAPI.Models.Order", b =>
                {
                    b.HasOne("MedFarmAPI.Models.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Orders_Clients");

                    b.HasOne("MedFarmAPI.Models.Drugstore", "Drugstores")
                        .WithMany("Orders")
                        .HasForeignKey("DrugstoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Orders_Drugstores");

                    b.Navigation("Client");

                    b.Navigation("Drugstores");
                });

            modelBuilder.Entity("MedFarmAPI.Models.Client", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("MedFarmAPI.Models.Doctor", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("MedFarmAPI.Models.Drugstore", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
