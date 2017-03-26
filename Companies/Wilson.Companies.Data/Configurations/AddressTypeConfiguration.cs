using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Companies.Core.Entities;

namespace Wilson.Companies.Data.Configurations
{
    public class AddressTypeConfiguration : EntityTypeConfiguration<Address>
    {
        public AddressTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<Address>());
        }

        public override void Map(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.Country).HasMaxLength(70).IsRequired();
            builder.Property(x => x.PostCode).HasMaxLength(10).IsRequired();
            builder.Property(x => x.City).HasMaxLength(70).IsRequired();
            builder.Property(x => x.Street).HasMaxLength(70).IsRequired();
            builder.Property(x => x.UnitNumber).HasMaxLength(6);
            builder.Property(x => x.Note).HasMaxLength(250);
            builder.HasMany<Company>().WithOne(x => x.Address).HasForeignKey(x => x.AddressId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany<Company>().WithOne(x => x.ShippingAddress).HasForeignKey(x => x.ShippingAddressId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany<Employee>().WithOne(x => x.Address).HasForeignKey(x => x.AddressId);
        }
    }
}
