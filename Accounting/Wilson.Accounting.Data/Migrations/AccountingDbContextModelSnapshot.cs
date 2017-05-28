using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Wilson.Accounting.Data;
using Wilson.Accounting.Core.Enumerations;

namespace Wilson.Accounting.Data.Migrations
{
    [DbContext(typeof(AccountingDbContext))]
    partial class AccountingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema("Accounting")
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Bill", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("BillItems");

                    b.Property<DateTime>("Date");

                    b.Property<bool>("HasInvoice");

                    b.Property<string>("HtmlContent")
                        .IsRequired();

                    b.Property<string>("InvoiceId")
                        .HasMaxLength(36);

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<DateTime>("RevisionDate");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Company", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("Address");

                    b.Property<bool>("HasVatRegistration");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<string>("OfficeEmail");

                    b.Property<string>("OfficePhone");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("VatNumber")
                        .HasMaxLength(12);

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Employee", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("CompanyId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<bool>("IsFired");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Invoice", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("BillId")
                        .HasMaxLength(36);

                    b.Property<string>("BuyerId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<DateTime?>("DateOfPayment");

                    b.Property<int>("DaysOfDelayedPayment");

                    b.Property<int>("InvoicePaymentType");

                    b.Property<int>("InvoiceType");

                    b.Property<int>("InvoiceVariant");

                    b.Property<bool>("IsPayed");

                    b.Property<DateTime>("IssueDate");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<decimal>("PayedAmount");

                    b.Property<DateTime>("PaymentDate");

                    b.Property<string>("Payments");

                    b.Property<string>("SellerId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<decimal>("SubTotal")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("Vat");

                    b.Property<decimal>("VatAmount")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("Id");

                    b.HasIndex("BillId")
                        .IsUnique();

                    b.HasIndex("BuyerId");

                    b.HasIndex("SellerId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.InvoiceItem", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("InvoiceId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,4)")
                        .HasMaxLength(36);

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ItemId");

                    b.ToTable("InvoiceItem");
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Item", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<int>("Мeasure");

                    b.HasKey("Id");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Paycheck", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<DateTime>("Date");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<int>("ExtraHours");

                    b.Property<DateTime>("From");

                    b.Property<int>("HourOnBusinessTrip");

                    b.Property<int>("HourOnHolidays");

                    b.Property<int>("Hours");

                    b.Property<bool>("IsPaied");

                    b.Property<int>("PaidDaysOff");

                    b.Property<decimal>("PayBusinessTrip")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("PayForExtraHours")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("PayForHolidayHours")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("PayForHours")
                        .HasColumnType("decimal(18,4)");

                    b.Property<decimal>("PayForPayedDaysOff")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("Payments");

                    b.Property<int>("SickDaysOff");

                    b.Property<DateTime>("To");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("UnpaidDaysOff");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Paycheck");
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Project", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(900);

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Storehouse", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("BillItems");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<string>("ProjectId")
                        .HasMaxLength(36);

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Storehouses");
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.StorehouseItem", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("InvoiceItemId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,4)")
                        .HasMaxLength(36);

                    b.Property<int>("Quantity");

                    b.Property<string>("StorehouseId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.HasKey("Id");

                    b.HasIndex("InvoiceItemId");

                    b.HasIndex("StorehouseId");

                    b.ToTable("StorehouseItem");
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Bill", b =>
                {
                    b.HasOne("Wilson.Accounting.Core.Entities.Project", "Project")
                        .WithMany("Bills")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Employee", b =>
                {
                    b.HasOne("Wilson.Accounting.Core.Entities.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Invoice", b =>
                {
                    b.HasOne("Wilson.Accounting.Core.Entities.Bill", "Bill")
                        .WithOne("Invoice")
                        .HasForeignKey("Wilson.Accounting.Core.Entities.Invoice", "BillId");

                    b.HasOne("Wilson.Accounting.Core.Entities.Company", "Buyer")
                        .WithMany("BuyInvoices")
                        .HasForeignKey("BuyerId");

                    b.HasOne("Wilson.Accounting.Core.Entities.Company", "Seller")
                        .WithMany("SaleInvoices")
                        .HasForeignKey("SellerId");
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.InvoiceItem", b =>
                {
                    b.HasOne("Wilson.Accounting.Core.Entities.Invoice", "Invoice")
                        .WithMany("InvoiceItems")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wilson.Accounting.Core.Entities.Item", "Item")
                        .WithMany("InvoiceItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Paycheck", b =>
                {
                    b.HasOne("Wilson.Accounting.Core.Entities.Employee", "Employee")
                        .WithMany("Paycheks")
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Project", b =>
                {
                    b.HasOne("Wilson.Accounting.Core.Entities.Company", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Storehouse", b =>
                {
                    b.HasOne("Wilson.Accounting.Core.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.StorehouseItem", b =>
                {
                    b.HasOne("Wilson.Accounting.Core.Entities.InvoiceItem", "InvoiceItem")
                        .WithMany("StorehouseItems")
                        .HasForeignKey("InvoiceItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wilson.Accounting.Core.Entities.Storehouse", "Storehouse")
                        .WithMany("StorehouseItems")
                        .HasForeignKey("StorehouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
