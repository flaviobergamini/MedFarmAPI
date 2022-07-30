using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedFarmAPI.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cpf = table.Column<string>(type: "NVARCHAR(14)", maxLength: 14, nullable: false),
                    State = table.Column<string>(type: "NVARCHAR(45)", maxLength: 45, nullable: false),
                    City = table.Column<string>(type: "NVARCHAR(45)", maxLength: 45, nullable: false),
                    Complement = table.Column<string>(type: "NVARCHAR(300)", maxLength: 300, nullable: true),
                    Cep = table.Column<string>(type: "NVARCHAR(9)", maxLength: 9, nullable: false),
                    Street = table.Column<string>(type: "NVARCHAR(300)", maxLength: 300, nullable: false),
                    StreetNumber = table.Column<int>(type: "INT", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "NVARCHAR(14)", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cpf = table.Column<string>(type: "NVARCHAR(14)", maxLength: 14, nullable: false),
                    Specialty = table.Column<string>(type: "NVARCHAR(30)", maxLength: 30, nullable: false),
                    RegionalCouncil = table.Column<string>(type: "NVARCHAR(30)", maxLength: 30, nullable: false),
                    State = table.Column<string>(type: "NVARCHAR(45)", maxLength: 45, nullable: false),
                    City = table.Column<string>(type: "NVARCHAR(45)", maxLength: 45, nullable: false),
                    Complement = table.Column<string>(type: "NVARCHAR(300)", maxLength: 300, nullable: true),
                    Cep = table.Column<string>(type: "NVARCHAR(9)", maxLength: 9, nullable: false),
                    Street = table.Column<string>(type: "NVARCHAR(300)", maxLength: 300, nullable: false),
                    StreetNumber = table.Column<int>(type: "INT", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "NVARCHAR(14)", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drugstore",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cnpj = table.Column<string>(type: "NVARCHAR(14)", maxLength: 14, nullable: false),
                    State = table.Column<string>(type: "NVARCHAR(45)", maxLength: 45, nullable: false),
                    City = table.Column<string>(type: "NVARCHAR(45)", maxLength: 45, nullable: false),
                    Complement = table.Column<string>(type: "NVARCHAR(300)", maxLength: 300, nullable: true),
                    Cep = table.Column<string>(type: "NVARCHAR(9)", maxLength: 9, nullable: false),
                    Street = table.Column<string>(type: "NVARCHAR(300)", maxLength: 300, nullable: false),
                    StreetNumber = table.Column<int>(type: "INT", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(80)", maxLength: 80, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "NVARCHAR(14)", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drugstore", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTimeAppointment = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Remote = table.Column<bool>(type: "BIT", nullable: false),
                    VideoCallUrl = table.Column<string>(type: "TEXT", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Clients",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctor",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "TEXT", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    DrugstoresId = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<string>(type: "NVARCHAR(45)", maxLength: 45, nullable: false),
                    City = table.Column<string>(type: "NVARCHAR(45)", maxLength: 45, nullable: false),
                    Complement = table.Column<string>(type: "NVARCHAR(300)", maxLength: 300, nullable: true),
                    Cep = table.Column<string>(type: "NVARCHAR(9)", maxLength: 9, nullable: false),
                    Street = table.Column<string>(type: "NVARCHAR(300)", maxLength: 300, nullable: false),
                    StreetNumber = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Clients",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Drugstores",
                        column: x => x.DrugstoresId,
                        principalTable: "Drugstore",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_ClientId",
                table: "Appointment",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_DoctorId",
                table: "Appointment",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ClientId",
                table: "Order",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_DrugstoresId",
                table: "Order",
                column: "DrugstoresId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Drugstore");
        }
    }
}
