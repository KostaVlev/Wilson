using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Wilson.Companies.Data;
using Wilson.Companies.Core.Enumerations;

namespace Wilson.Companies.Data.Migrations
{
    [DbContext(typeof(CompanyDbContext))]
    partial class CompanyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema("Companies")
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("EmployeeId")
                        .HasMaxLength(36);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Attachment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("ContractId")
                        .HasMaxLength(36);

                    b.Property<string>("Extention")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.Property<byte[]>("File")
                        .IsRequired();

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<string>("InfoRequestId")
                        .HasMaxLength(36);

                    b.Property<string>("InforequestResponseId")
                        .HasMaxLength(36);

                    b.Property<string>("InquiryId");

                    b.Property<long>("Size");

                    b.Property<DateTime>("UploadDate");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.HasIndex("InfoRequestId");

                    b.HasIndex("InforequestResponseId");

                    b.HasIndex("InquiryId");

                    b.ToTable("Attachment");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Company", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("Address");

                    b.Property<bool>("HasVatRegistration");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<string>("OfficeEmail")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<string>("OfficePhone")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("ShippingAddress");

                    b.Property<string>("VatNumber")
                        .HasMaxLength(12);

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.CompanyContract", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("CretedById")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<DateTime>("Date");

                    b.Property<string>("HtmlContent");

                    b.Property<bool>("IsApproved");

                    b.Property<DateTime?>("LastRevisedAt");

                    b.Property<string>("ProjectId")
                        .HasMaxLength(36);

                    b.Property<string>("RevisedById")
                        .HasMaxLength(36);

                    b.Property<int>("Revision");

                    b.HasKey("Id");

                    b.HasIndex("CretedById");

                    b.HasIndex("RevisedById");

                    b.ToTable("CompanyContract");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Employee", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("Address");

                    b.Property<string>("CompanyId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<int>("EmployeePosition");

                    b.Property<DateTime?>("FiredAt");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<DateTime>("HiredAt");

                    b.Property<bool>("IsFired");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<int?>("PositionBeforePromotion");

                    b.Property<string>("PrivatePhone")
                        .HasMaxLength(15);

                    b.Property<DateTime?>("PromotionDate");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.InfoRequest", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("InquiryId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<string>("RequestMessage")
                        .IsRequired()
                        .HasMaxLength(900);

                    b.Property<string>("ResponseMessage")
                        .HasMaxLength(900);

                    b.Property<DateTime?>("ResponseReceivedAt");

                    b.Property<DateTime>("SentAt");

                    b.Property<string>("SentById")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.HasKey("Id");

                    b.HasIndex("InquiryId");

                    b.HasIndex("SentById");

                    b.ToTable("InfoRequest");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Inquiry", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<DateTime?>("ClosedAt");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(900);

                    b.Property<DateTime>("ReceivedAt");

                    b.Property<string>("ReceivedById")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ReceivedById");

                    b.ToTable("Inquiries");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.InquiryEmployee", b =>
                {
                    b.Property<string>("InquiryId")
                        .HasMaxLength(36);

                    b.Property<string>("EmployeeId")
                        .HasMaxLength(36);

                    b.HasKey("InquiryId", "EmployeeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("InquiryEmployee");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Message", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasMaxLength(900);

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsNew")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<bool>("IsRecived");

                    b.Property<int>("MessageCategory");

                    b.Property<string>("RecipientId");

                    b.Property<DateTime?>("RecivedAt");

                    b.Property<string>("SenderId");

                    b.Property<DateTime>("SentAt");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.HasKey("Id");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Offer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<DateTime?>("ApprovedAt");

                    b.Property<string>("ContractId")
                        .HasMaxLength(36);

                    b.Property<string>("CreatedById");

                    b.Property<string>("HtmlContent")
                        .IsRequired();

                    b.Property<string>("InquiryId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<bool>("IsApproved");

                    b.Property<bool>("IsSent");

                    b.Property<DateTime?>("LastRevisedAt");

                    b.Property<int>("Revision");

                    b.Property<DateTime?>("SentAt");

                    b.Property<string>("SentById")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.HasIndex("CreatedById");

                    b.HasIndex("InquiryId");

                    b.HasIndex("SentById");

                    b.ToTable("Offer");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Project", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("ContractId")
                        .HasMaxLength(36);

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("Location");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(900);

                    b.HasKey("Id");

                    b.HasIndex("ContractId")
                        .IsUnique();

                    b.HasIndex("CustomerId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.RegistrationRequestMessage", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsNew")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<string>("PrivatePhone");

                    b.Property<DateTime?>("ReceivedAt");

                    b.Property<string>("RecipientId");

                    b.Property<DateTime>("SendAt");

                    b.HasKey("Id");

                    b.HasIndex("RecipientId");

                    b.ToTable("RegistrationRequestMessage");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Settings", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("HomeCompanyId");

                    b.Property<bool>("IsDatabaseInstalled");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Wilson.Companies.Core.Entities.ApplicationRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Wilson.Companies.Core.Entities.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Wilson.Companies.Core.Entities.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Wilson.Companies.Core.Entities.ApplicationRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wilson.Companies.Core.Entities.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.ApplicationUser", b =>
                {
                    b.HasOne("Wilson.Companies.Core.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Attachment", b =>
                {
                    b.HasOne("Wilson.Companies.Core.Entities.CompanyContract", "Cotract")
                        .WithMany("Attachments")
                        .HasForeignKey("ContractId");

                    b.HasOne("Wilson.Companies.Core.Entities.InfoRequest", "InfoRequest")
                        .WithMany("RequestAttachmnets")
                        .HasForeignKey("InfoRequestId");

                    b.HasOne("Wilson.Companies.Core.Entities.InfoRequest", "InforequestResponse")
                        .WithMany("ResponseAttachmnets")
                        .HasForeignKey("InforequestResponseId");

                    b.HasOne("Wilson.Companies.Core.Entities.Inquiry", "Inquiry")
                        .WithMany("Attachments")
                        .HasForeignKey("InquiryId");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.CompanyContract", b =>
                {
                    b.HasOne("Wilson.Companies.Core.Entities.Employee", "CretedBy")
                        .WithMany()
                        .HasForeignKey("CretedById");

                    b.HasOne("Wilson.Companies.Core.Entities.Employee", "RevisedBy")
                        .WithMany()
                        .HasForeignKey("RevisedById");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Employee", b =>
                {
                    b.HasOne("Wilson.Companies.Core.Entities.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.InfoRequest", b =>
                {
                    b.HasOne("Wilson.Companies.Core.Entities.Inquiry", "Inquiry")
                        .WithMany("InfoRequests")
                        .HasForeignKey("InquiryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wilson.Companies.Core.Entities.Employee", "SentBy")
                        .WithMany("InfoRequests")
                        .HasForeignKey("SentById")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Inquiry", b =>
                {
                    b.HasOne("Wilson.Companies.Core.Entities.Company", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("Wilson.Companies.Core.Entities.Employee", "ReceivedBy")
                        .WithMany()
                        .HasForeignKey("ReceivedById");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.InquiryEmployee", b =>
                {
                    b.HasOne("Wilson.Companies.Core.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.HasOne("Wilson.Companies.Core.Entities.Inquiry", "Inquiry")
                        .WithMany("Assignees")
                        .HasForeignKey("InquiryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Message", b =>
                {
                    b.HasOne("Wilson.Companies.Core.Entities.ApplicationUser", "Recipient")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("RecipientId");

                    b.HasOne("Wilson.Companies.Core.Entities.ApplicationUser", "Sender")
                        .WithMany("SentMessages")
                        .HasForeignKey("SenderId");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Offer", b =>
                {
                    b.HasOne("Wilson.Companies.Core.Entities.CompanyContract", "Contract")
                        .WithMany("Offers")
                        .HasForeignKey("ContractId");

                    b.HasOne("Wilson.Companies.Core.Entities.Employee", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById");

                    b.HasOne("Wilson.Companies.Core.Entities.Inquiry", "Inquiry")
                        .WithMany("Offers")
                        .HasForeignKey("InquiryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wilson.Companies.Core.Entities.Employee", "SentBy")
                        .WithMany()
                        .HasForeignKey("SentById");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Project", b =>
                {
                    b.HasOne("Wilson.Companies.Core.Entities.CompanyContract", "Contract")
                        .WithOne("Project")
                        .HasForeignKey("Wilson.Companies.Core.Entities.Project", "ContractId");

                    b.HasOne("Wilson.Companies.Core.Entities.Company", "Customer")
                        .WithMany("Projects")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.RegistrationRequestMessage", b =>
                {
                    b.HasOne("Wilson.Companies.Core.Entities.ApplicationUser", "Recipient")
                        .WithMany("RegistrationRequestMessages")
                        .HasForeignKey("RecipientId");
                });
        }
    }
}
