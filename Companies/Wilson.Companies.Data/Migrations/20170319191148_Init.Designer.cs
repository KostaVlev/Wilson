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
    [Migration("20170319191148_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema("Companies")
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

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

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<int?>("Floor");

                    b.Property<string>("Note")
                        .HasMaxLength(250);

                    b.Property<string>("PostCode")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<int>("StreetNumber");

                    b.Property<string>("UnitNumber")
                        .HasMaxLength(6);

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Attachment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ContractId");

                    b.Property<string>("Extention")
                        .IsRequired()
                        .HasMaxLength(4);

                    b.Property<byte[]>("File")
                        .IsRequired();

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<Guid?>("InfoRequestId");

                    b.Property<Guid?>("InforequestResponseId");

                    b.Property<Guid?>("InquiryId");

                    b.Property<DateTime>("UploadDate");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.HasIndex("InfoRequestId");

                    b.HasIndex("InforequestResponseId");

                    b.HasIndex("InquiryId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AddressId");

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

                    b.Property<Guid>("ShippingAddressId");

                    b.Property<string>("VatNumber")
                        .HasMaxLength(12);

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("ShippingAddressId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.CompanyContract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CretedById");

                    b.Property<DateTime>("Date");

                    b.Property<string>("HtmlContent");

                    b.Property<bool>("IsApproved");

                    b.Property<DateTime?>("LastRevisedAt");

                    b.Property<Guid>("ProjectId");

                    b.Property<int>("Revision");

                    b.HasKey("Id");

                    b.HasIndex("CretedById");

                    b.ToTable("CompanyContracts");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AddressId");

                    b.Property<Guid>("CompanyId");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<int>("EmployeePosition");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15);

                    b.Property<string>("PrivatePhone")
                        .HasMaxLength(15);

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.InfoRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("InquiryId");

                    b.Property<string>("RequestMessage")
                        .IsRequired()
                        .HasMaxLength(900);

                    b.Property<string>("ResponseMessage")
                        .HasMaxLength(900);

                    b.Property<DateTime?>("ResponseReceivedAt");

                    b.Property<DateTime>("SentAt");

                    b.Property<Guid>("SentById");

                    b.HasKey("Id");

                    b.HasIndex("InquiryId");

                    b.HasIndex("SentById");

                    b.ToTable("InfoRequests");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Inquiry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("ClosedAt");

                    b.Property<Guid>("CustomerId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(900);

                    b.Property<Guid>("ProjectId");

                    b.Property<DateTime>("ReceivedAt");

                    b.Property<Guid>("ReceivedById");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("ReceivedById");

                    b.ToTable("Inquiries");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.InquiryEmployee", b =>
                {
                    b.Property<Guid>("InquiryId");

                    b.Property<Guid>("EmployeeId");

                    b.HasKey("InquiryId", "EmployeeId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("InquiryEmployee");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Offer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("ApprovedAt");

                    b.Property<Guid?>("ContractId");

                    b.Property<string>("HtmlContent")
                        .IsRequired();

                    b.Property<Guid>("InquiryId");

                    b.Property<bool>("IsApproved");

                    b.Property<DateTime?>("LastRevisedAt");

                    b.Property<int>("Revision");

                    b.Property<DateTime>("SentAt");

                    b.Property<Guid>("SentById");

                    b.HasKey("Id");

                    b.HasIndex("ContractId");

                    b.HasIndex("InquiryId");

                    b.HasIndex("SentById");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ContractId");

                    b.Property<Guid>("CustomerId");

                    b.Property<Guid>("LocationId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(900);

                    b.HasKey("Id");

                    b.HasIndex("ContractId")
                        .IsUnique();

                    b.HasIndex("CustomerId");

                    b.HasIndex("LocationId")
                        .IsUnique();

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.ProjectLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<string>("Country")
                        .HasMaxLength(70);

                    b.Property<string>("Note")
                        .HasMaxLength(250);

                    b.Property<string>("PostCode");

                    b.Property<string>("Street")
                        .HasMaxLength(70);

                    b.Property<int?>("StreetNumber");

                    b.HasKey("Id");

                    b.ToTable("ProjectLocations");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

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

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Wilson.Companies.Core.Entities.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Wilson.Companies.Core.Entities.User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wilson.Companies.Core.Entities.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
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
                        .WithMany("Attachmnets")
                        .HasForeignKey("InquiryId");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Company", b =>
                {
                    b.HasOne("Wilson.Companies.Core.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("Wilson.Companies.Core.Entities.Address", "ShippingAddress")
                        .WithMany()
                        .HasForeignKey("ShippingAddressId");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.CompanyContract", b =>
                {
                    b.HasOne("Wilson.Companies.Core.Entities.Employee", "CretedBy")
                        .WithMany()
                        .HasForeignKey("CretedById");
                });

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Employee", b =>
                {
                    b.HasOne("Wilson.Companies.Core.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

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

                    b.HasOne("Wilson.Companies.Core.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.HasOne("Wilson.Companies.Core.Entities.Employee", "RecivedBy")
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

            modelBuilder.Entity("Wilson.Companies.Core.Entities.Offer", b =>
                {
                    b.HasOne("Wilson.Companies.Core.Entities.CompanyContract", "Contract")
                        .WithMany("Offers")
                        .HasForeignKey("ContractId");

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
                        .HasForeignKey("Wilson.Companies.Core.Entities.Project", "ContractId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wilson.Companies.Core.Entities.Company", "Customer")
                        .WithMany("Projects")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wilson.Companies.Core.Entities.ProjectLocation", "Location")
                        .WithOne()
                        .HasForeignKey("Wilson.Companies.Core.Entities.Project", "LocationId");
                });
        }
    }
}
