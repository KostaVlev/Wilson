using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Projects.Core.Entities;

namespace Wilson.Projects.Data.Configurations
{
    public class StorehouseItemBillTypeConfiguration : EntityTypeConfiguration<StorehouseItemBill>
    {
        public StorehouseItemBillTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<StorehouseItemBill>());
        }

        public override void Map(EntityTypeBuilder<StorehouseItemBill> builder)
        {
            builder.HasKey(x => new { x.StorehouseItemId, x.BillId });
            builder.Property(x => x.StorehouseItemId).HasMaxLength(36);
            builder.Property(x => x.BillId).HasMaxLength(36);
            builder.HasOne(x => x.Bill).WithMany(x => x.StorehouseItems).HasForeignKey(x => x.BillId);
            builder.HasOne(x => x.StorehouseItem).WithMany(x => x.Bills).HasForeignKey(x => x.StorehouseItemId).HasPrincipalKey(x => x.Id);
        }
    }
}
