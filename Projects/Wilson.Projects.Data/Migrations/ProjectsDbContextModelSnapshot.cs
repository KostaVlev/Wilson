using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Wilson.Projects.Data;
using Wilson.Projects.Core.Enumerations;

namespace Wilson.Projects.Data.Migrations
{
    [DbContext(typeof(ProjectsDbContext))]
    partial class ProjectsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema("Projects")
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Wilson.Projects.Core.Entities.Bill", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("CreatedById")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<DateTime>("Date");

                    b.Property<string>("HtmlContent");

                    b.Property<bool>("IsAccepted");

                    b.Property<bool>("IsApproved");

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ProjectId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("Wilson.Projects.Core.Entities.Employee", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Wilson.Projects.Core.Entities.Item", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<int>("Мeasure");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Wilson.Projects.Core.Entities.Project", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<DateTime>("ActualEndDate");

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("GuaranteePeriodInMonths");

                    b.Property<bool>("InProgress");

                    b.Property<string>("ManagerId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(900);

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("StorehouseId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Wilson.Projects.Core.Entities.Storehouse", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<string>("ProjectId")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.HasKey("Id");

                    b.HasIndex("ProjectId")
                        .IsUnique();

                    b.ToTable("Storehouses");
                });

            modelBuilder.Entity("Wilson.Projects.Core.Entities.StorehouseItem", b =>
                {
                    b.Property<string>("StorehouseId")
                        .HasMaxLength(36);

                    b.Property<string>("ItemId")
                        .HasMaxLength(36);

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasMaxLength(36);

                    b.Property<decimal>("Price");

                    b.Property<int>("Quantity");

                    b.HasKey("StorehouseId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("StorehouseItems");
                });

            modelBuilder.Entity("Wilson.Projects.Core.Entities.StorehouseItemBill", b =>
                {
                    b.Property<string>("StorehouseItemId")
                        .HasMaxLength(36);

                    b.Property<string>("BillId")
                        .HasMaxLength(36);

                    b.HasKey("StorehouseItemId", "BillId");

                    b.HasIndex("BillId");

                    b.ToTable("StorehouseItemBill");
                });

            modelBuilder.Entity("Wilson.Projects.Core.Entities.Bill", b =>
                {
                    b.HasOne("Wilson.Projects.Core.Entities.Employee", "CreatedBy")
                        .WithMany("Bills")
                        .HasForeignKey("CreatedById");

                    b.HasOne("Wilson.Projects.Core.Entities.Project", "Project")
                        .WithMany("Bills")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("Wilson.Projects.Core.Entities.Project", b =>
                {
                    b.HasOne("Wilson.Projects.Core.Entities.Employee", "Manager")
                        .WithMany("Projects")
                        .HasForeignKey("ManagerId");
                });

            modelBuilder.Entity("Wilson.Projects.Core.Entities.Storehouse", b =>
                {
                    b.HasOne("Wilson.Projects.Core.Entities.Project", "Project")
                        .WithOne("Storehouse")
                        .HasForeignKey("Wilson.Projects.Core.Entities.Storehouse", "ProjectId");
                });

            modelBuilder.Entity("Wilson.Projects.Core.Entities.StorehouseItem", b =>
                {
                    b.HasOne("Wilson.Projects.Core.Entities.Item", "Item")
                        .WithMany("Storehouses")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wilson.Projects.Core.Entities.Storehouse", "Storehouse")
                        .WithMany("Items")
                        .HasForeignKey("StorehouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Wilson.Projects.Core.Entities.StorehouseItemBill", b =>
                {
                    b.HasOne("Wilson.Projects.Core.Entities.Bill", "Bill")
                        .WithMany("StorehouseItems")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Wilson.Projects.Core.Entities.StorehouseItem", "StorehouseItem")
                        .WithMany("Bills")
                        .HasForeignKey("StorehouseItemId")
                        .HasPrincipalKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
