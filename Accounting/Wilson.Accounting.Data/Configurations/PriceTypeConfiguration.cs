using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Accounting.Core.Entities;
using Wilson.Accounting.Data.Extensions;

namespace Wilson.Accounting.Data.Configurations
{
    public class PriceTypeConfiguration : EntityTypeConfiguration<Price>
    {
        public PriceTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<Price>());
        }

        public override void Map(EntityTypeBuilder<Price> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Amount).HasPrecision(18, 4).IsRequired();
            builder.HasOne(x => x.Payment).WithOne(x => x.Price).HasForeignKey<Price>(x => x.PaymentId);
        }
    }
}
