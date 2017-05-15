using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wilson.Scheduler.Data.Migrations
{
    public partial class DDD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Scheduler");

            migrationBuilder.CreateTable(
                name: "PayRates",
                schema: "Scheduler",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    BusinessTripHour = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ExtraHour = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    HoidayHour = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Hour = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    IsBaseRate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                schema: "Scheduler",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Name = table.Column<string>(maxLength: 900, nullable: false),
                    ShortName = table.Column<string>(maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "Scheduler",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    EmployeePosition = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 70, nullable: false),
                    IsFired = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(maxLength: 70, nullable: false),
                    PayRateId = table.Column<string>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_PayRates_PayRateId",
                        column: x => x.PayRateId,
                        principalSchema: "Scheduler",
                        principalTable: "PayRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Paychecks",
                schema: "Scheduler",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<string>(maxLength: 36, nullable: false),
                    ExtraHours = table.Column<int>(nullable: false),
                    From = table.Column<DateTime>(nullable: false),
                    HourOnBusinessTrip = table.Column<int>(nullable: false),
                    HourOnHolidays = table.Column<int>(nullable: false),
                    Hours = table.Column<int>(nullable: false),
                    PaidDaysOff = table.Column<int>(nullable: false),
                    PayBusinessTrip = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PayForExtraHours = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PayForHolidayHours = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PayForHours = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PayForPayedDaysOff = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    SickDaysOff = table.Column<int>(nullable: false),
                    To = table.Column<DateTime>(nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    UnpaidDaysOff = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paychecks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paychecks_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Scheduler",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                schema: "Scheduler",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<string>(maxLength: 36, nullable: false),
                    ExtraWorkHours = table.Column<int>(nullable: false),
                    ProjectId = table.Column<string>(maxLength: 36, nullable: true),
                    ScheduleOption = table.Column<int>(nullable: false),
                    WorkHours = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Scheduler",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "Scheduler",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PayRateId",
                schema: "Scheduler",
                table: "Employees",
                column: "PayRateId");

            migrationBuilder.CreateIndex(
                name: "IX_Paychecks_EmployeeId",
                schema: "Scheduler",
                table: "Paychecks",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_EmployeeId",
                schema: "Scheduler",
                table: "Schedules",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ProjectId",
                schema: "Scheduler",
                table: "Schedules",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paychecks",
                schema: "Scheduler");

            migrationBuilder.DropTable(
                name: "Schedules",
                schema: "Scheduler");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "Scheduler");

            migrationBuilder.DropTable(
                name: "Projects",
                schema: "Scheduler");

            migrationBuilder.DropTable(
                name: "PayRates",
                schema: "Scheduler");
        }
    }
}
