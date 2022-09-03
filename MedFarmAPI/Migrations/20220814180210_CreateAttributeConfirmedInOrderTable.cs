using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedFarmAPI.Migrations
{
    public partial class CreateAttributeConfirmedInOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                table: "Appointment",
                type: "BIT",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirmed",
                table: "Appointment");
        }
    }
}
