using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Companies.Core.Entities;

namespace Wilson.Companies.Data.Configurations
{
    public class CompanyTypeConfiguration : EntityTypeConfiguration<Company>
    {
        public CompanyTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<Company>());
        }

        public override void Map(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.AddressId).HasMaxLength(36).IsRequired();
            builder.Property(x => x.ShippingAddressId).HasMaxLength(36).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(70).IsRequired();
            builder.Property(x => x.OfficeEmail).HasMaxLength(70).IsRequired();
            builder.Property(x => x.OfficePhone).HasMaxLength(15).IsRequired();
            builder.Property(x => x.RegistrationNumber).HasMaxLength(10).IsRequired();
            builder.Property(x => x.VatNumber).HasMaxLength(12);
            builder.HasMany(x => x.Employees).WithOne(x => x.Company).HasForeignKey(x => x.CompanyId);
            builder.HasMany(x => x.Projects).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerId);
        }
    }
}
