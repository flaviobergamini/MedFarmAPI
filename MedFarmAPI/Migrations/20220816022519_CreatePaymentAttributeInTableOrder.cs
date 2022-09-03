using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedFarmAPI.Migrations
{
    public partial class CreatePaymentAttributeInTableOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Payment",
                table: "Order",
                type: "NVARCHAR(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Payment",
                table: "Order");
        }
    }
}
