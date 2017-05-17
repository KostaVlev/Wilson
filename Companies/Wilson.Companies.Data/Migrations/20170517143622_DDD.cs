using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Wilson.Companies.Data.Migrations
{
    public partial class DDD : Migration
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
                name: "Companies",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Address = table.Column<string>(nullable: true),
                    HasVatRegistration = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 70, nullable: false),
                    OfficeEmail = table.Column<string>(maxLength: 70, nullable: false),
                    OfficePhone = table.Column<string>(maxLength: 15, nullable: false),
                    RegistrationNumber = table.Column<string>(maxLength: 10, nullable: false),
                    ShippingAddress = table.Column<string>(nullable: true),
                    VatNumber = table.Column<string>(maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    HomeCompanyId = table.Column<string>(nullable: true),
                    IsDatabaseInstalled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
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
                name: "Employees",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Address = table.Column<string>(nullable: true),
                    CompanyId = table.Column<string>(maxLength: 36, nullable: false),
                    Email = table.Column<string>(maxLength: 70, nullable: false),
                    EmployeePosition = table.Column<int>(nullable: false),
                    FiredAt = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 70, nullable: false),
                    HiredAt = table.Column<DateTime>(nullable: false),
                    IsFired = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(maxLength: 70, nullable: false),
                    Phone = table.Column<string>(maxLength: 15, nullable: false),
                    PositionBeforePromotion = table.Column<int>(nullable: true),
                    PrivatePhone = table.Column<string>(maxLength: 15, nullable: true),
                    PromotionDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Companies",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    EmployeeId = table.Column<string>(maxLength: 36, nullable: true),
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
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Companies",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyContract",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CretedById = table.Column<string>(maxLength: 36, nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    HtmlContent = table.Column<string>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    LastRevisedAt = table.Column<DateTime>(nullable: true),
                    ProjectId = table.Column<string>(maxLength: 36, nullable: true),
                    RevisedById = table.Column<string>(maxLength: 36, nullable: true),
                    Revision = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyContract", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyContract_Employees_CretedById",
                        column: x => x.CretedById,
                        principalSchema: "Companies",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompanyContract_Employees_RevisedById",
                        column: x => x.RevisedById,
                        principalSchema: "Companies",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inquiries",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    ClosedAt = table.Column<DateTime>(nullable: true),
                    CustomerId = table.Column<string>(maxLength: 36, nullable: false),
                    Description = table.Column<string>(maxLength: 900, nullable: false),
                    ReceivedAt = table.Column<DateTime>(nullable: false),
                    ReceivedById = table.Column<string>(maxLength: 36, nullable: false)
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
                        name: "FK_Inquiries_Employees_ReceivedById",
                        column: x => x.ReceivedById,
                        principalSchema: "Companies",
                        principalTable: "Employees",
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
                name: "Message",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Body = table.Column<string>(maxLength: 900, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsNew = table.Column<bool>(nullable: false, defaultValue: true),
                    IsRecived = table.Column<bool>(nullable: false),
                    MessageCategory = table.Column<int>(nullable: false),
                    RecipientId = table.Column<string>(nullable: true),
                    RecivedAt = table.Column<DateTime>(nullable: true),
                    SenderId = table.Column<string>(nullable: true),
                    SentAt = table.Column<DateTime>(nullable: false),
                    Subject = table.Column<string>(maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalSchema: "Companies",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "Companies",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegistrationRequestMessage",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 70, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsNew = table.Column<bool>(nullable: false, defaultValue: true),
                    LastName = table.Column<string>(maxLength: 70, nullable: false),
                    PrivatePhone = table.Column<string>(nullable: true),
                    ReceivedAt = table.Column<DateTime>(nullable: true),
                    RecipientId = table.Column<string>(nullable: true),
                    SendAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrationRequestMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrationRequestMessage_AspNetUsers_RecipientId",
                        column: x => x.RecipientId,
                        principalSchema: "Companies",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    ContractId = table.Column<string>(maxLength: 36, nullable: true),
                    CustomerId = table.Column<string>(maxLength: 36, nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Location = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 900, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_CompanyContract_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "Companies",
                        principalTable: "CompanyContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_Companies_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Companies",
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InfoRequest",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    InquiryId = table.Column<string>(maxLength: 36, nullable: false),
                    RequestMessage = table.Column<string>(maxLength: 900, nullable: false),
                    ResponseMessage = table.Column<string>(maxLength: 900, nullable: true),
                    ResponseReceivedAt = table.Column<DateTime>(nullable: true),
                    SentAt = table.Column<DateTime>(nullable: false),
                    SentById = table.Column<string>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfoRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InfoRequest_Inquiries_InquiryId",
                        column: x => x.InquiryId,
                        principalSchema: "Companies",
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfoRequest_Employees_SentById",
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
                    InquiryId = table.Column<string>(maxLength: 36, nullable: false),
                    EmployeeId = table.Column<string>(maxLength: 36, nullable: false)
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
                name: "Offer",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    ApprovedAt = table.Column<DateTime>(nullable: true),
                    ContractId = table.Column<string>(maxLength: 36, nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    HtmlContent = table.Column<string>(nullable: false),
                    InquiryId = table.Column<string>(maxLength: 36, nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    IsSent = table.Column<bool>(nullable: false),
                    LastRevisedAt = table.Column<DateTime>(nullable: true),
                    Revision = table.Column<int>(nullable: false),
                    SentAt = table.Column<DateTime>(nullable: true),
                    SentById = table.Column<string>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offer_CompanyContract_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "Companies",
                        principalTable: "CompanyContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offer_Employees_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "Companies",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offer_Inquiries_InquiryId",
                        column: x => x.InquiryId,
                        principalSchema: "Companies",
                        principalTable: "Inquiries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offer_Employees_SentById",
                        column: x => x.SentById,
                        principalSchema: "Companies",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                schema: "Companies",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    ContractId = table.Column<string>(maxLength: 36, nullable: true),
                    Extention = table.Column<string>(maxLength: 4, nullable: false),
                    File = table.Column<byte[]>(nullable: false),
                    FileName = table.Column<string>(maxLength: 70, nullable: false),
                    InfoRequestId = table.Column<string>(maxLength: 36, nullable: true),
                    InforequestResponseId = table.Column<string>(maxLength: 36, nullable: true),
                    InquiryId = table.Column<string>(nullable: true),
                    Size = table.Column<long>(nullable: false),
                    UploadDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_CompanyContract_ContractId",
                        column: x => x.ContractId,
                        principalSchema: "Companies",
                        principalTable: "CompanyContract",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachment_InfoRequest_InfoRequestId",
                        column: x => x.InfoRequestId,
                        principalSchema: "Companies",
                        principalTable: "InfoRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachment_InfoRequest_InforequestResponseId",
                        column: x => x.InforequestResponseId,
                        principalSchema: "Companies",
                        principalTable: "InfoRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachment_Inquiries_InquiryId",
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
                name: "IX_AspNetUsers_EmployeeId",
                schema: "Companies",
                table: "AspNetUsers",
                column: "EmployeeId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_ContractId",
                schema: "Companies",
                table: "Attachment",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_InfoRequestId",
                schema: "Companies",
                table: "Attachment",
                column: "InfoRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_InforequestResponseId",
                schema: "Companies",
                table: "Attachment",
                column: "InforequestResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_InquiryId",
                schema: "Companies",
                table: "Attachment",
                column: "InquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContract_CretedById",
                schema: "Companies",
                table: "CompanyContract",
                column: "CretedById");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContract_RevisedById",
                schema: "Companies",
                table: "CompanyContract",
                column: "RevisedById");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyId",
                schema: "Companies",
                table: "Employees",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoRequest_InquiryId",
                schema: "Companies",
                table: "InfoRequest",
                column: "InquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_InfoRequest_SentById",
                schema: "Companies",
                table: "InfoRequest",
                column: "SentById");

            migrationBuilder.CreateIndex(
                name: "IX_Inquiries_CustomerId",
                schema: "Companies",
                table: "Inquiries",
                column: "CustomerId");

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
                name: "IX_Message_RecipientId",
                schema: "Companies",
                table: "Message",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_SenderId",
                schema: "Companies",
                table: "Message",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_ContractId",
                schema: "Companies",
                table: "Offer",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_CreatedById",
                schema: "Companies",
                table: "Offer",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_InquiryId",
                schema: "Companies",
                table: "Offer",
                column: "InquiryId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_SentById",
                schema: "Companies",
                table: "Offer",
                column: "SentById");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ContractId",
                schema: "Companies",
                table: "Project",
                column: "ContractId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_CustomerId",
                schema: "Companies",
                table: "Project",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrationRequestMessage_RecipientId",
                schema: "Companies",
                table: "RegistrationRequestMessage",
                column: "RecipientId");
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
                name: "Attachment",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "InquiryEmployee",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "Message",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "Offer",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "Project",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "RegistrationRequestMessage",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "Settings",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "InfoRequest",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "CompanyContract",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "Inquiries",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "Companies");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "Companies");
        }
    }
}
