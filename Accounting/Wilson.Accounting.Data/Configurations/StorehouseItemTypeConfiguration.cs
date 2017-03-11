using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Accounting.Core.Entities;
using Microsoft.EntityFrameworkCore;

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
            builder.HasKey(x => new { x.ItemId, x.StorehouseId });
            builder.HasOne(x => x.Storehouse).WithMany(x => x.Items).HasForeignKey(x => x.ItemId);
            builder.HasOne(x => x.Item).WithMany(x => x.Storehouses).HasForeignKey(x => x.StorehouseId);
        }
    }
}
