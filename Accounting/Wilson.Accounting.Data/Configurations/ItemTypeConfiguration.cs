using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Accounting.Core.Entities;

namespace Wilson.Accounting.Data.Configurations
{
    public class ItemTypeConfiguration : EntityTypeConfiguration<Item>
    {
        public ItemTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<Item>());
        }

        public override void Map(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.Name).HasMaxLength(70).IsRequired();
            builder.HasMany(x => x.Prices).WithOne(x => x.Item).HasForeignKey(x => x.ItemId);
        }
    }
}
