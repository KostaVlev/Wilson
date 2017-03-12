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
                name: "Company");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "Company",
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
                schema: "Company",
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
                name: "Address",
                schema: "Company",
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
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyContract",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    HtmlContent = table.Column<string>(nullable: true),
                    ProjectId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyContract", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectLocation",
                schema: "Company",
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
                    table.PrimaryKey("PK_ProjectLocation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
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
                schema: "Company",
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
                        principalSchema: "Company",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                schema: "Company",
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
                    table.PrimaryKey("PK_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_Address_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "Company",
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Company_Address_ShippingAddressId",
                        column: x => x.ShippingAddressId,
                        principalSchema: "Company",
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "Company",
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
                        principalSchema: "Company",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "Company",
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
                        principalSchema: "Company",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "Company",
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
                        principalSchema: "Company",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Company",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                schema: "Company",
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
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Address_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "Company",
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employee_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Company",
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                schema: "Company",
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
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_CompanyContract_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "Company",
                        principalTable: "CompanyContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Project_Company_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Company",
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Project_ProjectLocation_LocationId",
                        column: x => x.LocationId,
                        principalSchema: "Company",
                        principalTable: "ProjectLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inquiry",
                schema: "Company",
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
                    table.PrimaryKey("PK_Inquiry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inquiry_Company_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Company",
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inquiry_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "Company",
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inquiry_Employee_ReceivedById",
                        column: x => x.ReceivedById,
                        principalSchema: "Company",
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InfoRequest",
                schema: "Company",
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
                    table.PrimaryKey("PK_InfoRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoRequest_Inquiry_InquiryId",
                        column: x => x.InquiryId,
                        principalSchema: "Company",
                        principalTable: "Inquiry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfoRequest_Employee_SentById",
                        column: x => x.SentById,
                        principalSchema: "Company",
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InquiryEmployee",
                schema: "Company",
                columns: table => new
                {
                    InquiryId = table.Column<Guid>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InquiryEmployee", x => new { x.InquiryId, x.EmployeeId });
                    table.ForeignKey(
                        name: "FK_InquiryEmployee_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Company",
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InquiryEmployee_Inquiry_InquiryId",
                        column: x => x.InquiryId,
                        principalSchema: "Company",
                        principalTable: "Inquiry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offer",
                schema: "Company",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ContractId = table.Column<Guid>(nullable: false),
                    HtmlContent = table.Column<string>(nullable: false),
                    InquiryId = table.Column<Guid>(nullable: false),
                    IsAccepted = table.Column<bool>(nullable: false),
                    Revision = table.Column<int>(nullable: false),
                    SentAt = table.Column<DateTime>(nullable: false),
                    SentById = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offer_CompanyContract_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "Company",
                        principalTable: "CompanyContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offer_Inquiry_InquiryId",
                        column: x => x.InquiryId,
                        principalSchema: "Company",
                        principalTable: "Inquiry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offer_Employee_SentById",
                        column: x => x.SentById,
                        principalSchema: "Company",
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                schema: "Company",
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
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_CompanyContract_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "Company",
                        principalTable: "CompanyContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachment_InfoRequest_InfoRequestId",
                        column: x => x.InfoRequestId,
                        principalSchema: "Company",
                        principalTable: "InfoRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachment_InfoRequest_InforequestResponseId",
                        column: x => x.InforequestResponseId,
                        principalSchema: "Company",
                        principalTable: "InfoRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachment_Inquiry_InquiryId",
                        column: x => x.InquiryId,
                        principalSchema: "Company",
                        principalTable: "Inquiry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Company",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "Company",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "Company",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "Company",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "Company",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_ContractId",
                schema: "Company",
                table: "Attachment",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_InfoRequestId",
                schema: "Company",
                table: "Attachment",
                column: "InfoRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_InforequestResponseId",
                schema: "Company",
                table: "Attachment",
                column: "InforequestResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_InquiryId",
                schema: "Company",
                table: "Attachment",
                column: "InquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_AddressId",
                schema: "Company",
                table: "Company",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_ShippingAddressId",
                schema: "Company",
                table: "Company",
                column: "ShippingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_AddressId",
                schema: "Company",
                table: "Employee",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_CompanyId",
                schema: "Company",
                table: "Employee",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoRequest_InquiryId",
                schema: "Company",
                table: "InfoRequest",
                column: "InquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoRequest_SentById",
                schema: "Company",
                table: "InfoRequest",
                column: "SentById");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiry_CustomerId",
                schema: "Company",
                table: "Inquiry",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiry_ProjectId",
                schema: "Company",
                table: "Inquiry",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiry_ReceivedById",
                schema: "Company",
                table: "Inquiry",
                column: "ReceivedById");

            migrationBuilder.CreateIndex(
                name: "IX_InquiryEmployee_EmployeeId",
                schema: "Company",
                table: "InquiryEmployee",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_ContractId",
                schema: "Company",
                table: "Offer",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_InquiryId",
                schema: "Company",
                table: "Offer",
                column: "InquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_SentById",
                schema: "Company",
                table: "Offer",
                column: "SentById");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ContractId",
                schema: "Company",
                table: "Project",
                column: "ContractId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_CustomerId",
                schema: "Company",
                table: "Project",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_LocationId",
                schema: "Company",
                table: "Project",
                column: "LocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Company",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Company",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "Attachment",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "InquiryEmployee",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "Offer",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "InfoRequest",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "Inquiry",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "Project",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "Employee",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "CompanyContract",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "ProjectLocation",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "Company",
                schema: "Company");

            migrationBuilder.DropTable(
                name: "Address",
                schema: "Company");
        }
    }
}
