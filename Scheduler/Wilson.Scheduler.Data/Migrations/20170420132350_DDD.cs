using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wilson.Scheduler.Data.Migrations
{
    public partial class DDD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HourOnBusinessTrip",
                schema: "Scheduler",
                table: "Paychecks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PayBusinessTrip",
                schema: "Scheduler",
                table: "Paychecks",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HourOnBusinessTrip",
                schema: "Scheduler",
                table: "Paychecks");

            migrationBuilder.DropColumn(
                name: "PayBusinessTrip",
                schema: "Scheduler",
                table: "Paychecks");
        }
    }
}
