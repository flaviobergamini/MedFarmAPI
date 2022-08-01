using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedFarmAPI.Migrations
{
    public partial class ModifySizeCnpj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "Drugstore",
                type: "NVARCHAR(18)",
                maxLength: 18,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(14)",
                oldMaxLength: 14);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "Drugstore",
                type: "NVARCHAR(14)",
                maxLength: 14,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(18)",
                oldMaxLength: 18);
        }
    }
}
