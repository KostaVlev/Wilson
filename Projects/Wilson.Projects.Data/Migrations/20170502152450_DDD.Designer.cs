using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Wilson.Projects.Data;

namespace Wilson.Projects.Data.Migrations
{
    [DbContext(typeof(ProjectsDbContext))]
    [Migration("20170502152450_DDD")]
    partial class DDD
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasDefaultSchema("Projects")
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Wilson.Projects.Core.Entities.Company", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.HasKey("Id");

                    b.ToTable("Company");
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

            modelBuilder.Entity("Wilson.Projects.Core.Entities.Project", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36);

                    b.Property<DateTime?>("ActualEndDate");

                    b.Property<string>("CustomerId");

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

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ManagerId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Wilson.Projects.Core.Entities.Project", b =>
                {
                    b.HasOne("Wilson.Projects.Core.Entities.Company", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("Wilson.Projects.Core.Entities.Employee", "Manager")
                        .WithMany("Projects")
                        .HasForeignKey("ManagerId");
                });
        }
    }
}
