using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Accounting.Core.Entities;

namespace Wilson.Accounting.Data.Configurations
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
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.CustomerId).HasMaxLength(36).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(900).IsRequired();
            builder.HasOne(x => x.Customer).WithMany().HasForeignKey(x => x.CustomerId);
        }
    }
}
