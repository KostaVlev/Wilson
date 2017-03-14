using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wilson.Companies.Core.Entities;
using Wilson.Companies.Data.Configurations;

namespace Wilson.Companies.Data
{
    public class CompanyDbContext : IdentityDbContext<User>
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<CompanyContract> CompanyContracts { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<InfoRequest> InfoRequests { get; set; }
        public virtual DbSet<Inquiry> Inquiries { get; set; }        
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectLocation> ProjectLocations { get; set; }

        public virtual DbSet<InquiryEmployee> InquiryEmployee { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Companies");
            this.RegesterEntityTypeConfigurations(builder);

            base.OnModelCreating(builder);
        }

        private void RegesterEntityTypeConfigurations(ModelBuilder builder)
        {
            var typesToRegister = Assembly.Load(new AssemblyName("Wilson.Company.Data")).GetTypes().Where(
                type => type.GetTypeInfo().BaseType != null &&
                !type.GetTypeInfo().IsAbstract &&
                typeof(IEntityTypeConfiguration).IsAssignableFrom(type));

            foreach (var type in typesToRegister)
            {
                Activator.CreateInstance(type, builder);
            }
        }
    }
}
