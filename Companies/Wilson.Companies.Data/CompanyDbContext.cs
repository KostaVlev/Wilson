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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Company");
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
