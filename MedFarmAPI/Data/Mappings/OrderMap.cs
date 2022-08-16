using MedFarmAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedFarmAPI.Data.Mappings
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            // Chave Primária
            builder.HasKey(x => x.Id);
            //Identity
            builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();

            //atributos da tabela

            builder.Property(x => x.DateTimeOrder)
            .IsRequired()
            .HasColumnName("DateTimeOrder")
            .HasColumnType("DATETIME");

            builder.Property(x => x.Image)
            .IsRequired()
            .HasColumnName("Image")
            .HasColumnType("TEXT");

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
            .HasColumnName("Complement")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(300);

            builder.Property(x => x.District)
            .IsRequired(true)
            .HasColumnName("District")
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

            builder.Property(x => x.Payment)
            .IsRequired(true)
            .HasColumnName("Payment")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(20);

            builder.HasOne(x => x.Client).WithMany(x => x.Orders)
                .HasConstraintName("FK_Orders_Clients")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Drugstores).WithMany(x => x.Orders)
                .HasConstraintName("FK_Orders_Drugstores")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
