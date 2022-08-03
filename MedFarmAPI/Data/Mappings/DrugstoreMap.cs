using MedFarmAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedFarmAPI.Data.Mappings
{
    public class DrugstoreMap : IEntityTypeConfiguration<Drugstore>
    {
        public void Configure(EntityTypeBuilder<Drugstore> builder)
        {
            builder.ToTable("Drugstore");

            // Chave Primária
            builder.HasKey(x => x.Id);
            //Identity
            builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();

            //atributos da tabela

            builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(80);

            builder.Property(x => x.Cnpj)
            .IsRequired()
            .HasColumnName("Cnpj")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(18);

            builder.Property(x => x.Email)
            .IsRequired()
            .HasColumnName("Email")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(200);

            builder.Property(x => x.State)
            .IsRequired()
            .HasColumnName("State")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(45);

            builder.Property(x => x.City)
            .IsRequired()
            .HasColumnName("City")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(45);

            builder.Property(x => x.Complement)
            .IsRequired(false)
            .HasColumnName("Complement")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(300);

            builder.Property(x => x.Cep)
            .IsRequired()
            .HasColumnName("Cep")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(9);

            builder.Property(x => x.StreetNumber)
            .IsRequired()
            .HasColumnName("StreetNumber")
            .HasColumnType("INT");

            builder.Property(x => x.Street)
            .IsRequired()
            .HasColumnName("Street")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(300);

            builder.Property(x => x.Phone)
            .IsRequired()
            .HasColumnName("Phone")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(14);

            builder.Property(x => x.Password)
            .IsRequired()
            .HasColumnName("PasswordHash")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(1000);

            builder.Property(x => x.Roles)
            .IsRequired()
            .HasColumnName("Roles")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(20);
        }
    }
}
