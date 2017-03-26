using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Accounting.Core.Entities;

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
            builder.HasKey(x => new { x.InvoiceItemId, x.StorehouseId });
            builder.Property(x => x.StorehouseId).HasMaxLength(36);
            builder.Property(x => x.InvoiceItemId).HasMaxLength(36);
            builder.HasOne(x => x.Storehouse).WithMany(x => x.Items).HasForeignKey(x => x.StorehouseId);
            builder.HasOne(x => x.InvoiceItem).WithMany(x => x.Storehouses).HasForeignKey(x => x.InvoiceItemId);
        }
    }
}
