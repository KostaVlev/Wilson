using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wilson.Projects.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Projects");

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 70, nullable: false),
                    LastName = table.Column<string>(maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                schema: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 70, nullable: false),
                    Мeasure = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                schema: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ActualEndDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    GuaranteePeriodInMonths = table.Column<int>(nullable: false),
                    InProgress = table.Column<bool>(nullable: false),
                    ManagerId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 900, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    StorehouseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalSchema: "Projects",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                schema: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    HtmlContent = table.Column<string>(nullable: true),
                    IsAccepted = table.Column<bool>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Employees_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "Projects",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bills_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "Projects",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Storehouses",
                schema: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 70, nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Storehouses_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "Projects",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StorehouseItems",
                schema: "Projects",
                columns: table => new
                {
                    StorehouseId = table.Column<Guid>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorehouseItems", x => new { x.StorehouseId, x.ItemId });
                    table.UniqueConstraint("AK_StorehouseItems_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StorehouseItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalSchema: "Projects",
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StorehouseItems_Storehouses_StorehouseId",
                        column: x => x.StorehouseId,
                        principalSchema: "Projects",
                        principalTable: "Storehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StorehouseItemBill",
                schema: "Projects",
                columns: table => new
                {
                    StorehouseItemId = table.Column<Guid>(nullable: false),
                    BillId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorehouseItemBill", x => new { x.StorehouseItemId, x.BillId });
                    table.ForeignKey(
                        name: "FK_StorehouseItemBill_Bills_BillId",
                        column: x => x.BillId,
                        principalSchema: "Projects",
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StorehouseItemBill_StorehouseItems_StorehouseItemId",
                        column: x => x.StorehouseItemId,
                        principalSchema: "Projects",
                        principalTable: "StorehouseItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_CreatedById",
                schema: "Projects",
                table: "Bills",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_ProjectId",
                schema: "Projects",
                table: "Bills",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ManagerId",
                schema: "Projects",
                table: "Projects",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Storehouses_ProjectId",
                schema: "Projects",
                table: "Storehouses",
                column: "ProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StorehouseItems_ItemId",
                schema: "Projects",
                table: "StorehouseItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_StorehouseItemBill_BillId",
                schema: "Projects",
                table: "StorehouseItemBill",
                column: "BillId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StorehouseItemBill",
                schema: "Projects");

            migrationBuilder.DropTable(
                name: "Bills",
                schema: "Projects");

            migrationBuilder.DropTable(
                name: "StorehouseItems",
                schema: "Projects");

            migrationBuilder.DropTable(
                name: "Items",
                schema: "Projects");

            migrationBuilder.DropTable(
                name: "Storehouses",
                schema: "Projects");

            migrationBuilder.DropTable(
                name: "Projects",
                schema: "Projects");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "Projects");
        }
    }
}
