using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wilson.Accounting.Data.Migrations
{
    public partial class DDD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Projects_ProjectId",
                schema: "Accounting",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Addresses_AddressId",
                schema: "Accounting",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItems_Invoices_InvoiceId",
                schema: "Accounting",
                table: "InvoiceItems");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItems_Items_ItemId",
                schema: "Accounting",
                table: "InvoiceItems");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItems_Prices_PriceId",
                schema: "Accounting",
                table: "InvoiceItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Companies_CustomerId",
                schema: "Accounting",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Storehouses_Projects_ProjectId",
                schema: "Accounting",
                table: "Storehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_StorehouseItems_InvoiceItems_InvoiceItemId",
                schema: "Accounting",
                table: "StorehouseItems");

            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "Prices",
                schema: "Accounting");

            migrationBuilder.DropTable(
                name: "Payments",
                schema: "Accounting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StorehouseItems",
                schema: "Accounting",
                table: "StorehouseItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                schema: "Accounting",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                schema: "Accounting",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceItems",
                schema: "Accounting",
                table: "InvoiceItems");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceItems_PriceId",
                schema: "Accounting",
                table: "InvoiceItems");

            migrationBuilder.DropIndex(
                name: "IX_Companies_AddressId",
                schema: "Accounting",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "Accounting",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "PriceId",
                schema: "Accounting",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "AddressId",
                schema: "Accounting",
                table: "Companies");

            migrationBuilder.RenameTable(
                name: "Projects",
                schema: "Accounting",
                newName: "Project");

            migrationBuilder.RenameTable(
                name: "Items",
                schema: "Accounting",
                newName: "Item");

            migrationBuilder.RenameTable(
                name: "InvoiceItems",
                schema: "Accounting",
                newName: "InvoiceItem");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_CustomerId",
                schema: "Accounting",
                table: "Project",
                newName: "IX_Project_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceItems_ItemId",
                schema: "Accounting",
                table: "InvoiceItem",
                newName: "IX_InvoiceItem_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceItems_InvoiceId",
                schema: "Accounting",
                table: "InvoiceItem",
                newName: "IX_InvoiceItem_InvoiceId");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                schema: "Accounting",
                table: "StorehouseItems",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "Accounting",
                table: "StorehouseItems",
                type: "decimal(18,4)",
                maxLength: 36,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                schema: "Accounting",
                table: "StorehouseItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ItemId",
                schema: "Accounting",
                table: "InvoiceItem",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "Accounting",
                table: "InvoiceItem",
                type: "decimal(18,4)",
                maxLength: 36,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                schema: "Accounting",
                table: "Invoices",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Payments",
                schema: "Accounting",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFired",
                schema: "Accounting",
                table: "Employees",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "Accounting",
                table: "Companies",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasInvoice",
                schema: "Accounting",
                table: "Bills",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StorehouseItems",
                schema: "Accounting",
                table: "StorehouseItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                schema: "Accounting",
                table: "Project",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item",
                schema: "Accounting",
                table: "Item",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceItem",
                schema: "Accounting",
                table: "InvoiceItem",
                column: "Id");

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
                        name: "FK_Paycheck_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Accounting",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StorehouseItems_InvoiceItemId",
                schema: "Accounting",
                table: "StorehouseItems",
                column: "InvoiceItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Paycheck_EmployeeId",
                schema: "Accounting",
                table: "Paycheck",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Project_ProjectId",
                schema: "Accounting",
                table: "Bills",
                column: "ProjectId",
                principalSchema: "Accounting",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItem_Invoices_InvoiceId",
                schema: "Accounting",
                table: "InvoiceItem",
                column: "InvoiceId",
                principalSchema: "Accounting",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItem_Item_ItemId",
                schema: "Accounting",
                table: "InvoiceItem",
                column: "ItemId",
                principalSchema: "Accounting",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Companies_CustomerId",
                schema: "Accounting",
                table: "Project",
                column: "CustomerId",
                principalSchema: "Accounting",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Storehouses_Project_ProjectId",
                schema: "Accounting",
                table: "Storehouses",
                column: "ProjectId",
                principalSchema: "Accounting",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StorehouseItems_InvoiceItem_InvoiceItemId",
                schema: "Accounting",
                table: "StorehouseItems",
                column: "InvoiceItemId",
                principalSchema: "Accounting",
                principalTable: "InvoiceItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Project_ProjectId",
                schema: "Accounting",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItem_Invoices_InvoiceId",
                schema: "Accounting",
                table: "InvoiceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceItem_Item_ItemId",
                schema: "Accounting",
                table: "InvoiceItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Companies_CustomerId",
                schema: "Accounting",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Storehouses_Project_ProjectId",
                schema: "Accounting",
                table: "Storehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_StorehouseItems_InvoiceItem_InvoiceItemId",
                schema: "Accounting",
                table: "StorehouseItems");

            migrationBuilder.DropTable(
                name: "Paycheck",
                schema: "Accounting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StorehouseItems",
                schema: "Accounting",
                table: "StorehouseItems");

            migrationBuilder.DropIndex(
                name: "IX_StorehouseItems_InvoiceItemId",
                schema: "Accounting",
                table: "StorehouseItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                schema: "Accounting",
                table: "Project");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item",
                schema: "Accounting",
                table: "Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceItem",
                schema: "Accounting",
                table: "InvoiceItem");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "Accounting",
                table: "StorehouseItems");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "Accounting",
                table: "StorehouseItems");

            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "Accounting",
                table: "StorehouseItems");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "Accounting",
                table: "InvoiceItem");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                schema: "Accounting",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Payments",
                schema: "Accounting",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "IsFired",
                schema: "Accounting",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Address",
                schema: "Accounting",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "HasInvoice",
                schema: "Accounting",
                table: "Bills");

            migrationBuilder.RenameTable(
                name: "Project",
                schema: "Accounting",
                newName: "Projects");

            migrationBuilder.RenameTable(
                name: "Item",
                schema: "Accounting",
                newName: "Items");

            migrationBuilder.RenameTable(
                name: "InvoiceItem",
                schema: "Accounting",
                newName: "InvoiceItems");

            migrationBuilder.RenameIndex(
                name: "IX_Project_CustomerId",
                schema: "Accounting",
                table: "Projects",
                newName: "IX_Projects_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceItem_ItemId",
                schema: "Accounting",
                table: "InvoiceItems",
                newName: "IX_InvoiceItems_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceItem_InvoiceId",
                schema: "Accounting",
                table: "InvoiceItems",
                newName: "IX_InvoiceItems_InvoiceId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                schema: "Accounting",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "ItemId",
                schema: "Accounting",
                table: "InvoiceItems",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 36);

            migrationBuilder.AddColumn<string>(
                name: "PriceId",
                schema: "Accounting",
                table: "InvoiceItems",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AddressId",
                schema: "Accounting",
                table: "Companies",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StorehouseItems",
                schema: "Accounting",
                table: "StorehouseItems",
                columns: new[] { "InvoiceItemId", "StorehouseId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                schema: "Accounting",
                table: "Projects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                schema: "Accounting",
                table: "Items",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceItems",
                schema: "Accounting",
                table: "InvoiceItems",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    City = table.Column<string>(maxLength: 70, nullable: false),
                    Country = table.Column<string>(maxLength: 70, nullable: false),
                    Floor = table.Column<int>(nullable: true),
                    Note = table.Column<string>(maxLength: 250, nullable: true),
                    PostCode = table.Column<string>(maxLength: 10, nullable: false),
                    Street = table.Column<string>(maxLength: 70, nullable: false),
                    StreetNumber = table.Column<string>(maxLength: 6, nullable: false),
                    UnitNumber = table.Column<string>(maxLength: 6, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    InvoiceId = table.Column<string>(maxLength: 36, nullable: false),
                    PriceId = table.Column<string>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalSchema: "Accounting",
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                schema: "Accounting",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ItemId = table.Column<string>(maxLength: 36, nullable: true),
                    PaymentId = table.Column<string>(maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prices_Items_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "Accounting",
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prices_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalSchema: "Accounting",
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItems_PriceId",
                schema: "Accounting",
                table: "InvoiceItems",
                column: "PriceId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_AddressId",
                schema: "Accounting",
                table: "Companies",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_InvoiceId",
                schema: "Accounting",
                table: "Payments",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_ItemId",
                schema: "Accounting",
                table: "Prices",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_PaymentId",
                schema: "Accounting",
                table: "Prices",
                column: "PaymentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Projects_ProjectId",
                schema: "Accounting",
                table: "Bills",
                column: "ProjectId",
                principalSchema: "Accounting",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Addresses_AddressId",
                schema: "Accounting",
                table: "Companies",
                column: "AddressId",
                principalSchema: "Accounting",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItems_Invoices_InvoiceId",
                schema: "Accounting",
                table: "InvoiceItems",
                column: "InvoiceId",
                principalSchema: "Accounting",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItems_Items_ItemId",
                schema: "Accounting",
                table: "InvoiceItems",
                column: "ItemId",
                principalSchema: "Accounting",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceItems_Prices_PriceId",
                schema: "Accounting",
                table: "InvoiceItems",
                column: "PriceId",
                principalSchema: "Accounting",
                principalTable: "Prices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Companies_CustomerId",
                schema: "Accounting",
                table: "Projects",
                column: "CustomerId",
                principalSchema: "Accounting",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Storehouses_Projects_ProjectId",
                schema: "Accounting",
                table: "Storehouses",
                column: "ProjectId",
                principalSchema: "Accounting",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StorehouseItems_InvoiceItems_InvoiceItemId",
                schema: "Accounting",
                table: "StorehouseItems",
                column: "InvoiceItemId",
                principalSchema: "Accounting",
                principalTable: "InvoiceItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
