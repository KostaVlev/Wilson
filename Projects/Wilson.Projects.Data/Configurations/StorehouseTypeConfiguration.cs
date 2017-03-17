using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Projects.Core.Entities;

namespace Wilson.Projects.Data.Configurations
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
            builder.HasOne(x => x.Project).WithOne(x => x.Storehouse).HasForeignKey<Storehouse>(x => x.ProjectId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
