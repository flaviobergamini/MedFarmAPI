using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedFarmAPI.Migrations
{
    public partial class CreateDataOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeOrder",
                table: "Order",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateTimeOrder",
                table: "Order");
        }
    }
}
