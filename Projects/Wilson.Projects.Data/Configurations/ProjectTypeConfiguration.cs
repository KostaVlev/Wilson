using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Projects.Core.Entities;

namespace Wilson.Projects.Data.Configurations
{
    public class ProjectTypeConfiguration : EntityTypeConfiguration<Project>
    {
        public ProjectTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<Project>());
        }

        public override void Map(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(900).IsRequired();
            builder.HasOne(x => x.Storehouse).WithOne(x => x.Project).HasForeignKey<Project>(x => x.StorehouseId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Bills).WithOne(x => x.Project).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
