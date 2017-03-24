using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Wilson.Companies.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Companies");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "Companies",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    City = table.Column<string>(maxLength: 70, nullable: false),
                    Country = table.Column<string>(maxLength: 70, nullable: false),
                    Floor = table.Column<int>(nullable: true),
                    Note = table.Column<string>(maxLength: 250, nullable: true),
                    PostCode = table.Column<string>(maxLength: 10, nullable: false),
                    Street = table.Column<string>(maxLength: 70, nullable: false),
                    StreetNumber = table.Column<int>(nullable: false),
                    UnitNumber = table.Column<string>(maxLength: 6, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectLocations",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    City = table.Column<string>(maxLength: 70, nullable: false),
                    Country = table.Column<string>(maxLength: 70, nullable: true),
                    Note = table.Column<string>(maxLength: 250, nullable: true),
                    PostCode = table.Column<string>(nullable: true),
                    Street = table.Column<string>(maxLength: 70, nullable: true),
                    StreetNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 70, nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    LastName = table.Column<string>(maxLength: 70, nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Companies",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AddressId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 70, nullable: false),
                    OfficeEmail = table.Column<string>(maxLength: 70, nullable: false),
                    OfficePhone = table.Column<string>(maxLength: 15, nullable: false),
                    RegistrationNumber = table.Column<string>(maxLength: 10, nullable: false),
                    ShippingAddressId = table.Column<Guid>(nullable: false),
                    VatNumber = table.Column<string>(maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "Companies",
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Addresses_ShippingAddressId",
                        column: x => x.ShippingAddressId,
                        principalSchema: "Companies",
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Companies",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "Companies",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Companies",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "Companies",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Companies",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Companies",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AddressId = table.Column<Guid>(nullable: true),
                    CompanyId = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(maxLength: 70, nullable: false),
                    EmployeePosition = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 70, nullable: false),
                    LastName = table.Column<string>(maxLength: 70, nullable: false),
                    Phone = table.Column<string>(maxLength: 15, nullable: false),
                    PrivatePhone = table.Column<string>(maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "Companies",
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Companies",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyContracts",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CretedById = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    HtmlContent = table.Column<string>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    LastRevisedAt = table.Column<DateTime>(nullable: true),
                    ProjectId = table.Column<Guid>(nullable: false),
                    Revision = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyContracts_Employees_CretedById",
                        column: x => x.CretedById,
                        principalSchema: "Companies",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContractId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    LocationId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 900, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_CompanyContracts_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "Companies",
                        principalTable: "CompanyContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_Companies_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Companies",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Projects_ProjectLocations_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "Companies",
                        principalTable: "ProjectLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inquiries",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClosedAt = table.Column<DateTime>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 900, nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    ReceivedAt = table.Column<DateTime>(nullable: false),
                    ReceivedById = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inquiries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inquiries_Companies_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Companies",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inquiries_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "Companies",
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inquiries_Employees_ReceivedById",
                        column: x => x.ReceivedById,
                        principalSchema: "Companies",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InfoRequests",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    InquiryId = table.Column<Guid>(nullable: false),
                    RequestMessage = table.Column<string>(maxLength: 900, nullable: false),
                    ResponseMessage = table.Column<string>(maxLength: 900, nullable: true),
                    ResponseReceivedAt = table.Column<DateTime>(nullable: true),
                    SentAt = table.Column<DateTime>(nullable: false),
                    SentById = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoRequests_Inquiries_InquiryId",
                        column: x => x.InquiryId,
                        principalSchema: "Companies",
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfoRequests_Employees_SentById",
                        column: x => x.SentById,
                        principalSchema: "Companies",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InquiryEmployee",
                schema: "Companies",
                columns: table => new
                {
                    InquiryId = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryEmployee", x => new { x.InquiryId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_InquiryEmployee_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Companies",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InquiryEmployee_Inquiries_InquiryId",
                        column: x => x.InquiryId,
                        principalSchema: "Companies",
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ApprovedAt = table.Column<DateTime>(nullable: true),
                    ContractId = table.Column<Guid>(nullable: true),
                    HtmlContent = table.Column<string>(nullable: false),
                    InquiryId = table.Column<Guid>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    LastRevisedAt = table.Column<DateTime>(nullable: true),
                    Revision = table.Column<int>(nullable: false),
                    SentAt = table.Column<DateTime>(nullable: false),
                    SentById = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_CompanyContracts_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "Companies",
                        principalTable: "CompanyContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offers_Inquiries_InquiryId",
                        column: x => x.InquiryId,
                        principalSchema: "Companies",
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offers_Employees_SentById",
                        column: x => x.SentById,
                        principalSchema: "Companies",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContractId = table.Column<Guid>(nullable: true),
                    Extention = table.Column<string>(maxLength: 4, nullable: false),
                    File = table.Column<byte[]>(nullable: false),
                    FileName = table.Column<string>(maxLength: 70, nullable: false),
                    InfoRequestId = table.Column<Guid>(nullable: true),
                    InforequestResponseId = table.Column<Guid>(nullable: true),
                    InquiryId = table.Column<Guid>(nullable: true),
                    UploadDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_CompanyContracts_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "Companies",
                        principalTable: "CompanyContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachments_InfoRequests_InfoRequestId",
                        column: x => x.InfoRequestId,
                        principalSchema: "Companies",
                        principalTable: "InfoRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachments_InfoRequests_InforequestResponseId",
                        column: x => x.InforequestResponseId,
                        principalSchema: "Companies",
                        principalTable: "InfoRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachments_Inquiries_InquiryId",
                        column: x => x.InquiryId,
                        principalSchema: "Companies",
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Companies",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "Companies",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "Companies",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "Companies",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "Companies",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_ContractId",
                schema: "Companies",
                table: "Attachments",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_InfoRequestId",
                schema: "Companies",
                table: "Attachments",
                column: "InfoRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_InforequestResponseId",
                schema: "Companies",
                table: "Attachments",
                column: "InforequestResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_InquiryId",
                schema: "Companies",
                table: "Attachments",
                column: "InquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_AddressId",
                schema: "Companies",
                table: "Companies",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ShippingAddressId",
                schema: "Companies",
                table: "Companies",
                column: "ShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContracts_CretedById",
                schema: "Companies",
                table: "CompanyContracts",
                column: "CretedById");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AddressId",
                schema: "Companies",
                table: "Employees",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyId",
                schema: "Companies",
                table: "Employees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoRequests_InquiryId",
                schema: "Companies",
                table: "InfoRequests",
                column: "InquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoRequests_SentById",
                schema: "Companies",
                table: "InfoRequests",
                column: "SentById");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_CustomerId",
                schema: "Companies",
                table: "Inquiries",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_ProjectId",
                schema: "Companies",
                table: "Inquiries",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_ReceivedById",
                schema: "Companies",
                table: "Inquiries",
                column: "ReceivedById");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryEmployee_EmployeeId",
                schema: "Companies",
                table: "InquiryEmployee",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_ContractId",
                schema: "Companies",
                table: "Offers",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_InquiryId",
                schema: "Companies",
                table: "Offers",
                column: "InquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_SentById",
                schema: "Companies",
                table: "Offers",
                column: "SentById");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ContractId",
                schema: "Companies",
                table: "Projects",
                column: "ContractId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CustomerId",
                schema: "Companies",
                table: "Projects",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_LocationId",
                schema: "Companies",
                table: "Projects",
                column: "LocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Companies",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Companies",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "Attachments",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "InquiryEmployee",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "Offers",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "InfoRequests",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "Inquiries",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "Projects",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "CompanyContracts",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "ProjectLocations",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "Addresses",
                schema: "Companies");
        }
    }
}
