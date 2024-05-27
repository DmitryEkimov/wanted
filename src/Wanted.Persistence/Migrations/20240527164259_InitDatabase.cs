namespace Wanted.Persistence.Migrations;

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

/// <inheritdoc />
public partial class InitDatabase : Migration
{
    private static readonly string[] Columns = ["Id", "Name"];

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Companies",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Companies", x => x.Id)
        );

        migrationBuilder.CreateTable(
            name: "Employees",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                FirstName = table.Column<string>(type: "text", nullable: false),
                LastName = table.Column<string>(type: "text", nullable: false),
                SurName = table.Column<string>(type: "text", nullable: true),
                Email = table.Column<string>(type: "text", nullable: false),
                Number = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Employees", x => x.Id)
        );

        migrationBuilder.CreateTable(
            name: "CompanyEmployees",
            columns: table => new
            {
                CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                EmployeeId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CompanyEmployees", x => new { x.CompanyId, x.EmployeeId });
                table.ForeignKey(
                    name: "FK_CompanyEmployees_Companies_CompanyId",
                    column: x => x.CompanyId,
                    principalTable: "Companies",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
                table.ForeignKey(
                    name: "FK_CompanyEmployees_Employees_EmployeeId",
                    column: x => x.EmployeeId,
                    principalTable: "Employees",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade
                );
            }
        );

        migrationBuilder.InsertData(
            table: "Companies",
            columns: Columns,
            values: new object[,]
            {
                { new Guid("1b0f8308-feb0-4d55-93ec-0765971e0bb7"), "Ozon" },
                { new Guid("26f40fdf-8f92-4c4f-80c1-71090d86aef4"), "Gazprom" },
                { new Guid("5f7f47e3-610f-499c-9119-b73e1df23b62"), "Wildberries" },
                { new Guid("804b81bf-1730-43c8-aeeb-de6685e41cc3"), "Yandex" }
            }
        );

        migrationBuilder.CreateIndex(
            name: "IX_CompanyEmployees_EmployeeId",
            table: "CompanyEmployees",
            column: "EmployeeId"
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "CompanyEmployees");

        migrationBuilder.DropTable(name: "Companies");

        migrationBuilder.DropTable(name: "Employees");
    }
}
