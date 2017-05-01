using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Accounting.Core.Entities;
using Wilson.Accounting.Data.Extensions;

namespace Wilson.Accounting.Data.Configurations
{
    public class StorehouseItemTypeConfiguration : EntityTypeConfiguration<StorehouseItem>
    {
        public StorehouseItemTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<StorehouseItem>());
        }

        public override void Map(EntityTypeBuilder<StorehouseItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.StorehouseId).HasMaxLength(36).IsRequired();
            builder.Property(x => x.InvoiceItemId).HasMaxLength(36).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Price).HasMaxLength(36).HasPrecision(18, 4).IsRequired();
            builder.HasOne(x => x.Storehouse).WithMany(x => x.StorehouseItems).HasForeignKey(x => x.StorehouseId);
            builder.HasOne(x => x.InvoiceItem).WithMany(x => x.StorehouseItems).HasForeignKey(x => x.InvoiceItemId);
        }
    }
}
