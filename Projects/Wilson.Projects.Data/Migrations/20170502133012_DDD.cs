using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wilson.Projects.Data.Migrations
{
    public partial class DDD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Projects");

            migrationBuilder.CreateTable(
                name: "Company",
                schema: "Projects",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Name = table.Column<string>(maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "Projects",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    FirstName = table.Column<string>(maxLength: 70, nullable: false),
                    LastName = table.Column<string>(maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                schema: "Projects",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    ActualEndDate = table.Column<DateTime>(nullable: true),
                    CustomerId = table.Column<string>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false),
                    GuaranteePeriodInMonths = table.Column<int>(nullable: false),
                    InProgress = table.Column<bool>(nullable: false),
                    ManagerId = table.Column<string>(maxLength: 36, nullable: false),
                    Name = table.Column<string>(maxLength: 900, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    StorehouseId = table.Column<string>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Company_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Projects",
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalSchema: "Projects",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CustomerId",
                schema: "Projects",
                table: "Projects",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ManagerId",
                schema: "Projects",
                table: "Projects",
                column: "ManagerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects",
                schema: "Projects");

            migrationBuilder.DropTable(
                name: "Company",
                schema: "Projects");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "Projects");
        }
    }
}
