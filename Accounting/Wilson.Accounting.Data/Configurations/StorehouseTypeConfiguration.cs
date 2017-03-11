using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Accounting.Core.Entities;

namespace Wilson.Accounting.Data.Configurations
{
    public class StorehouseTypeConfiguration : EntityTypeConfiguration<Storehouse>
    {
        public StorehouseTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<Storehouse>());
        }

        public override void Map(EntityTypeBuilder<Storehouse> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(70).IsRequired();
        }
    }
}
