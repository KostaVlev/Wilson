using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wilson.Companies.Data.Migrations
{
    public partial class AddSettingsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    IsDatabaseInstalled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings",
                schema: "Companies");
        }
    }
}
