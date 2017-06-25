using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wilson.Accounting.Data.Migrations
{
    public partial class Paycheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraHours",
                schema: "Accounting",
                table: "Paycheck");

            migrationBuilder.DropColumn(
                name: "From",
                schema: "Accounting",
                table: "Paycheck");

            migrationBuilder.DropColumn(
                name: "HourOnBusinessTrip",
                schema: "Accounting",
                table: "Paycheck");

            migrationBuilder.DropColumn(
                name: "HourOnHolidays",
                schema: "Accounting",
                table: "Paycheck");

            migrationBuilder.DropColumn(
                name: "Hours",
                schema: "Accounting",
                table: "Paycheck");

            migrationBuilder.DropColumn(
                name: "PaidDaysOff",
                schema: "Accounting",
                table: "Paycheck");

            migrationBuilder.DropColumn(
                name: "PayBusinessTrip",
                schema: "Accounting",
                table: "Paycheck");

            migrationBuilder.DropColumn(
                name: "PayForExtraHours",
                schema: "Accounting",
                table: "Paycheck");

            migrationBuilder.DropColumn(
                name: "PayForHolidayHours",
                schema: "Accounting",
                table: "Paycheck");

            migrationBuilder.DropColumn(
                name: "PayForHours",
                schema: "Accounting",
                table: "Paycheck");

            migrationBuilder.DropColumn(
                name: "PayForPayedDaysOff",
                schema: "Accounting",
                table: "Paycheck");

            migrationBuilder.DropColumn(
                name: "SickDaysOff",
                schema: "Accounting",
                table: "Paycheck");

            migrationBuilder.DropColumn(
                name: "To",
                schema: "Accounting",
                table: "Paycheck");

            migrationBuilder.DropColumn(
                name: "UnpaidDaysOff",
                schema: "Accounting",
                table: "Paycheck");

            migrationBuilder.AddColumn<string>(
                name: "DaysOff",
                schema: "Accounting",
                table: "Paycheck",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PayRate",
                schema: "Accounting",
                table: "Paycheck",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Period",
                schema: "Accounting",
                table: "Paycheck",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubTotals",
                schema: "Accounting",
                table: "Paycheck",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "WorkingHours",
                schema: "Accounting",
                table: "Paycheck",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysOff",
                schema: "Accounting",
                table: "Paycheck");

            migrationBuilder.DropColumn(
                name: "PayRate",
                schema: "Accounting",
                table: "Paycheck");

            migrationBuilder.DropColumn(
                name: "Period",
                schema: "Accounting",
                table: "Paycheck");

            migrationBuilder.DropColumn(
                name: "SubTotals",
                schema: "Accounting",
                table: "Paycheck");

            migrationBuilder.DropColumn(
                name: "WorkingHours",
                schema: "Accounting",
                table: "Paycheck");

            migrationBuilder.AddColumn<int>(
                name: "ExtraHours",
                schema: "Accounting",
                table: "Paycheck",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "From",
                schema: "Accounting",
                table: "Paycheck",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "HourOnBusinessTrip",
                schema: "Accounting",
                table: "Paycheck",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HourOnHolidays",
                schema: "Accounting",
                table: "Paycheck",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Hours",
                schema: "Accounting",
                table: "Paycheck",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PaidDaysOff",
                schema: "Accounting",
                table: "Paycheck",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "PayBusinessTrip",
                schema: "Accounting",
                table: "Paycheck",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PayForExtraHours",
                schema: "Accounting",
                table: "Paycheck",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PayForHolidayHours",
                schema: "Accounting",
                table: "Paycheck",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PayForHours",
                schema: "Accounting",
                table: "Paycheck",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PayForPayedDaysOff",
                schema: "Accounting",
                table: "Paycheck",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "SickDaysOff",
                schema: "Accounting",
                table: "Paycheck",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "To",
                schema: "Accounting",
                table: "Paycheck",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UnpaidDaysOff",
                schema: "Accounting",
                table: "Paycheck",
                nullable: false,
                defaultValue: 0);
        }
    }
}
