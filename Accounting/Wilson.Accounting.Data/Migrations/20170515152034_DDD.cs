using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wilson.Accounting.Data.Migrations
{
    public partial class DDD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Accounting");

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Address = table.Column<string>(nullable: true),
                    HasVatRegistration = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 70, nullable: false),
                    RegistrationNumber = table.Column<string>(maxLength: 10, nullable: false),
                    VatNumber = table.Column<string>(maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Name = table.Column<string>(maxLength: 70, nullable: false),
                    Мeasure = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CompanyId = table.Column<string>(maxLength: 36, nullable: false),
                    FirstName = table.Column<string>(maxLength: 70, nullable: false),
                    IsFired = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Accounting",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CustomerId = table.Column<string>(maxLength: 36, nullable: false),
                    Name = table.Column<string>(maxLength: 900, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_Companies_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Accounting",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Paycheck",
                schema: "Accounting",
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
                    IsPaied = table.Column<bool>(nullable: false),
                    PaidDaysOff = table.Column<int>(nullable: false),
                    PayBusinessTrip = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PayForExtraHours = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PayForHolidayHours = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PayForHours = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PayForPayedDaysOff = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Payments = table.Column<string>(nullable: true),
                    SickDaysOff = table.Column<int>(nullable: false),
                    To = table.Column<DateTime>(nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    UnpaidDaysOff = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paycheck", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paycheck_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Accounting",
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    BillItems = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    HasInvoice = table.Column<bool>(nullable: false),
                    HtmlContent = table.Column<string>(nullable: false),
                    InvoiceId = table.Column<string>(maxLength: 36, nullable: true),
                    ProjectId = table.Column<string>(maxLength: 36, nullable: false),
                    RevisionDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "Accounting",
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Storehouses",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    BillItems = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 70, nullable: false),
                    ProjectId = table.Column<string>(maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Storehouses_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "Accounting",
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    BillId = table.Column<string>(maxLength: 36, nullable: true),
                    BuyerId = table.Column<string>(maxLength: 36, nullable: false),
                    DateOfPayment = table.Column<DateTime>(nullable: true),
                    DaysOfDelayedPayment = table.Column<int>(nullable: false),
                    InvoicePaymentType = table.Column<int>(nullable: false),
                    InvoiceType = table.Column<int>(nullable: false),
                    InvoiceVariant = table.Column<int>(nullable: false),
                    IsPayed = table.Column<bool>(nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    Number = table.Column<string>(maxLength: 10, nullable: false),
                    PayedAmount = table.Column<decimal>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    Payments = table.Column<string>(nullable: true),
                    SellerId = table.Column<string>(maxLength: 36, nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Vat = table.Column<int>(nullable: false),
                    VatAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Bills_BillId",
                        column: x => x.BillId,
                        principalSchema: "Accounting",
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Companies_BuyerId",
                        column: x => x.BuyerId,
                        principalSchema: "Accounting",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Companies_SellerId",
                        column: x => x.SellerId,
                        principalSchema: "Accounting",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceItem",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    InvoiceId = table.Column<string>(maxLength: 36, nullable: false),
                    ItemId = table.Column<string>(maxLength: 36, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", maxLength: 36, nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceItem_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "Accounting",
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "Accounting",
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StorehouseItem",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    InvoiceItemId = table.Column<string>(maxLength: 36, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", maxLength: 36, nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    StorehouseId = table.Column<string>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorehouseItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorehouseItem_InvoiceItem_InvoiceItemId",
                        column: x => x.InvoiceItemId,
                        principalSchema: "Accounting",
                        principalTable: "InvoiceItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StorehouseItem_Storehouses_StorehouseId",
                        column: x => x.StorehouseId,
                        principalSchema: "Accounting",
                        principalTable: "Storehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_ProjectId",
                schema: "Accounting",
                table: "Bills",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_CompanyId",
                schema: "Accounting",
                table: "Employee",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BillId",
                schema: "Accounting",
                table: "Invoices",
                column: "BillId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BuyerId",
                schema: "Accounting",
                table: "Invoices",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_SellerId",
                schema: "Accounting",
                table: "Invoices",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_InvoiceId",
                schema: "Accounting",
                table: "InvoiceItem",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_ItemId",
                schema: "Accounting",
                table: "InvoiceItem",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Paycheck_EmployeeId",
                schema: "Accounting",
                table: "Paycheck",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_CustomerId",
                schema: "Accounting",
                table: "Project",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Storehouses_ProjectId",
                schema: "Accounting",
                table: "Storehouses",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StorehouseItem_InvoiceItemId",
                schema: "Accounting",
                table: "StorehouseItem",
                column: "InvoiceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_StorehouseItem_StorehouseId",
                schema: "Accounting",
                table: "StorehouseItem",
                column: "StorehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paycheck",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "StorehouseItem",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "Employee",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "InvoiceItem",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "Storehouses",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "Invoices",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "Item",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "Bills",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "Project",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "Accounting");
        }
    }
}
