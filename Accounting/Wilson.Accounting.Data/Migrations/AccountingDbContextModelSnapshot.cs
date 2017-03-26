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
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Address", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

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

                    b.Property<string>("Town")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<string>("UnitNumber")
                        .HasMaxLength(6);

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Bill", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<DateTime>("Date");

                    b.Property<string>("HtmlContent")
                        .IsRequired();

                    b.Property<string>("InvoiceId")
                        .HasMaxLength(36);

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Company", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("AddressId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<bool>("HasVatRegistration");

                    b.Property<string>("IndetificationNumber")
                        .IsRequired()
                        .HasMaxLength(9);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<string>("VatNumber")
                        .HasMaxLength(11);

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

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

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Employees");
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
                    b.Property<string>("InvoiceId")
                        .HasMaxLength(36);

                    b.Property<string>("ItemId");

                    b.Property<string>("PriceId")
                        .HasMaxLength(36);

                    b.Property<int>("Quantity");

                    b.HasKey("InvoiceId", "ItemId", "PriceId");

                    b.HasIndex("ItemId");

                    b.HasIndex("PriceId");

                    b.ToTable("InvoiceItems");
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Item", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<int>("Quantity");

                    b.Property<int>("Мeasure");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Payment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<DateTime>("Date");

                    b.Property<string>("InvoiceId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<string>("PriceId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Price", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("ItemId")
                        .HasMaxLength(36);

                    b.Property<string>("PaymentId")
                        .HasMaxLength(36);

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("PaymentId")
                        .IsUnique();

                    b.ToTable("Prices");
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

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Storehouse", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.HasKey("Id");

                    b.ToTable("Storehouses");
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.StorehouseItem", b =>
                {
                    b.Property<string>("ItemId")
                        .HasMaxLength(36);

                    b.Property<string>("StorehouseId")
                        .HasMaxLength(36);

                    b.HasKey("ItemId", "StorehouseId");

                    b.HasIndex("StorehouseId");

                    b.ToTable("StorehouseItems");
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Bill", b =>
                {
                    b.HasOne("Wilson.Accounting.Core.Entities.Project", "Project")
                        .WithMany("Bills")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Company", b =>
                {
                    b.HasOne("Wilson.Accounting.Core.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
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
                        .WithMany("Items")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wilson.Accounting.Core.Entities.Item", "Item")
                        .WithMany("Invoices")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wilson.Accounting.Core.Entities.Price", "Price")
                        .WithMany()
                        .HasForeignKey("PriceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Payment", b =>
                {
                    b.HasOne("Wilson.Accounting.Core.Entities.Invoice", "Invoice")
                        .WithMany("Payments")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Price", b =>
                {
                    b.HasOne("Wilson.Accounting.Core.Entities.Item", "Item")
                        .WithMany("Prices")
                        .HasForeignKey("ItemId");

                    b.HasOne("Wilson.Accounting.Core.Entities.Payment", "Payment")
                        .WithOne("Price")
                        .HasForeignKey("Wilson.Accounting.Core.Entities.Price", "PaymentId");
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.Project", b =>
                {
                    b.HasOne("Wilson.Accounting.Core.Entities.Company", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wilson.Accounting.Core.Entities.StorehouseItem", b =>
                {
                    b.HasOne("Wilson.Accounting.Core.Entities.Storehouse", "Storehouse")
                        .WithMany("Items")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wilson.Accounting.Core.Entities.Item", "Item")
                        .WithMany("Storehouses")
                        .HasForeignKey("StorehouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
