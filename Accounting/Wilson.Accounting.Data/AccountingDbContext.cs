using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Wilson.Accounting.Core.Entities;
using Wilson.Accounting.Data.Configurations;

namespace Wilson.Accounting.Data
{
    public class AccountingDbContext : DbContext
    {
        public AccountingDbContext(DbContextOptions<AccountingDbContext> options) 
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Storehouse> Storehouses { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Accounting");
            this.RegesterEntityTypeConfigurations(builder);

            base.OnModelCreating(builder);
        }

        private void RegesterEntityTypeConfigurations(ModelBuilder builder)
        {
            var typesToRegister = Assembly.Load(new AssemblyName("Wilson.Accounting.Data")).GetTypes().Where(
                type => type.GetTypeInfo().BaseType != null &&
                !type.GetTypeInfo().IsAbstract &&
                typeof(IEntityTypeConfiguration).IsAssignableFrom(type));

            foreach (var type in typesToRegister)
            {
                Activator.CreateInstance(type, builder);
            }
        }

        //private void RegesterEntities(ModelBuilder builder)
        //{
        //    var typesToRegester = Assembly.Load(new AssemblyName("Wilson.Accounting.Core")).GetTypes()
        //        .Where(e => !e.GetTypeInfo().IsAbstract && typeof(IEntity).IsAssignableFrom(e) && e.GetTypeInfo().IsClass);

        //    foreach (var type in typesToRegester)
        //    {
        //        var entity = Activator.CreateInstance(type);
        //        if (builder.Model.FindEntityType(type) == null)
        //        {
        //            builder.Model.AddEntityType(type);
        //        }
        //    }
        //}
    }
}
