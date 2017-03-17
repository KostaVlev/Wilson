using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Projects.Core.Entities;

namespace Wilson.Projects.Data.Configurations
{
    public class StorehouseItmeTypeConfiguration : EntityTypeConfiguration<StorehouseItem>
    {
        public StorehouseItmeTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<StorehouseItem>());
        }

        public override void Map(EntityTypeBuilder<StorehouseItem> builder)
        {
            builder.HasKey(x => new { x.StorehouseId, x.ItemId });
            builder.Property(x => x.Id).IsRequired();
            builder.HasOne(x => x.Item).WithMany(x => x.Storehouses).HasForeignKey(x => x.ItemId);
            builder.HasOne(x => x.Storehouse).WithMany(x => x.Items).HasForeignKey(x => x.StorehouseId);
        }
    }
}
