using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSSQLDataGenerator.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayScales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MinSalary = table.Column<long>(type: "bigint", nullable: false),
                    MaxSalary = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayScales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Phone_number = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date_of_Joining = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pay_Scale_Fk = table.Column<int>(type: "int", nullable: false),
                    Department_id_Fk = table.Column<int>(type: "int", nullable: false),
                    Location_id_Fk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_Department_id_Fk",
                        column: x => x.Department_id_Fk,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_PayScales_Pay_Scale_Fk",
                        column: x => x.Pay_Scale_Fk,
                        principalTable: "PayScales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_locations_Location_id_Fk",
                        column: x => x.Location_id_Fk,
                        principalTable: "locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Name",
                table: "Departments",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Department_id_Fk",
                table: "Employees",
                column: "Department_id_Fk");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Location_id_Fk",
                table: "Employees",
                column: "Location_id_Fk");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Pay_Scale_Fk",
                table: "Employees",
                column: "Pay_Scale_Fk");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Phone_number",
                table: "Employees",
                column: "Phone_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_locations_City",
                table: "locations",
                column: "City",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PayScales_Code",
                table: "PayScales",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "PayScales");

            migrationBuilder.DropTable(
                name: "locations");
        }
    }
}
