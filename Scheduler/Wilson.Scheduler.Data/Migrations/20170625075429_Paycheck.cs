using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wilson.Scheduler.Data.Migrations
{
    public partial class Paycheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraHours",
                schema: "Scheduler",
                table: "Paychecks");

            migrationBuilder.DropColumn(
                name: "From",
                schema: "Scheduler",
                table: "Paychecks");

            migrationBuilder.DropColumn(
                name: "HourOnBusinessTrip",
                schema: "Scheduler",
                table: "Paychecks");

            migrationBuilder.DropColumn(
                name: "HourOnHolidays",
                schema: "Scheduler",
                table: "Paychecks");

            migrationBuilder.DropColumn(
                name: "Hours",
                schema: "Scheduler",
                table: "Paychecks");

            migrationBuilder.DropColumn(
                name: "PaidDaysOff",
                schema: "Scheduler",
                table: "Paychecks");

            migrationBuilder.DropColumn(
                name: "PayBusinessTrip",
                schema: "Scheduler",
                table: "Paychecks");

            migrationBuilder.DropColumn(
                name: "PayForExtraHours",
                schema: "Scheduler",
                table: "Paychecks");

            migrationBuilder.DropColumn(
                name: "PayForHolidayHours",
                schema: "Scheduler",
                table: "Paychecks");

            migrationBuilder.DropColumn(
                name: "PayForHours",
                schema: "Scheduler",
                table: "Paychecks");

            migrationBuilder.DropColumn(
                name: "PayForPayedDaysOff",
                schema: "Scheduler",
                table: "Paychecks");

            migrationBuilder.DropColumn(
                name: "SickDaysOff",
                schema: "Scheduler",
                table: "Paychecks");

            migrationBuilder.DropColumn(
                name: "To",
                schema: "Scheduler",
                table: "Paychecks");

            migrationBuilder.DropColumn(
                name: "UnpaidDaysOff",
                schema: "Scheduler",
                table: "Paychecks");

            migrationBuilder.AddColumn<string>(
                name: "DaysOff",
                schema: "Scheduler",
                table: "Paychecks",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Period",
                schema: "Scheduler",
                table: "Paychecks",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubTotals",
                schema: "Scheduler",
                table: "Paychecks",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkingHours",
                schema: "Scheduler",
                table: "Paychecks",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysOff",
                schema: "Scheduler",
                table: "Paychecks");

            migrationBuilder.DropColumn(
                name: "Period",
                schema: "Scheduler",
                table: "Paychecks");

            migrationBuilder.DropColumn(
                name: "SubTotals",
                schema: "Scheduler",
                table: "Paychecks");

            migrationBuilder.DropColumn(
                name: "WorkingHours",
                schema: "Scheduler",
                table: "Paychecks");

            migrationBuilder.AddColumn<int>(
                name: "ExtraHours",
                schema: "Scheduler",
                table: "Paychecks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "From",
                schema: "Scheduler",
                table: "Paychecks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "HourOnBusinessTrip",
                schema: "Scheduler",
                table: "Paychecks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HourOnHolidays",
                schema: "Scheduler",
                table: "Paychecks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Hours",
                schema: "Scheduler",
                table: "Paychecks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaidDaysOff",
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

            migrationBuilder.AddColumn<decimal>(
                name: "PayForExtraHours",
                schema: "Scheduler",
                table: "Paychecks",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PayForHolidayHours",
                schema: "Scheduler",
                table: "Paychecks",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PayForHours",
                schema: "Scheduler",
                table: "Paychecks",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PayForPayedDaysOff",
                schema: "Scheduler",
                table: "Paychecks",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "SickDaysOff",
                schema: "Scheduler",
                table: "Paychecks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "To",
                schema: "Scheduler",
                table: "Paychecks",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UnpaidDaysOff",
                schema: "Scheduler",
                table: "Paychecks",
                nullable: false,
                defaultValue: 0);
        }
    }
}
