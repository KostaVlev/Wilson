using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Accounting.Core.Entities;

namespace Wilson.Accounting.Data.Configurations
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
            builder.Property(x => x.StreetNumber).HasMaxLength(6).IsRequired();
            builder.Property(x => x.UnitNumber).HasMaxLength(6);
            builder.Property(x => x.Note).HasMaxLength(250);
        }
    }
}
