using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeApi.DataContext.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Pays",
                columns: table => new
                {
                    PayId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PIO = table.Column<float>(type: "real", nullable: false, computedColumnSql: "[BrutoPay]*24/100", stored: true),
                    Insurance = table.Column<float>(type: "real", nullable: false, computedColumnSql: "[BrutoPay]*5.15/100", stored: true),
                    UnemployeementPlan = table.Column<float>(type: "real", nullable: false, computedColumnSql: "[BrutoPay]*0.75/100", stored: true),
                    Tax = table.Column<float>(type: "real", nullable: false, computedColumnSql: "[BrutoPay]*10/100", stored: true),
                    BrutoPay = table.Column<float>(type: "real", nullable: false),
                    NetoPay = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pays", x => x.PayId);
                    table.ForeignKey(
                        name: "FK_Pays_Employees_PayId",
                        column: x => x.PayId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Address", "LastName", "Name" },
                values: new object[,]
                {
                    { new Guid("7f558ec4-4b30-4467-89b1-b2f1a991ab55"), "addd", "Doe", "John" },
                    { new Guid("c56524ec-677c-4391-887c-90b8204d9bbe"), "addd", "Johnes", "Tom" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pays");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
