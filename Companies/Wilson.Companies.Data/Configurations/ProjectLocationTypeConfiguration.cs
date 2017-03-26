using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Companies.Core.Entities;

namespace Wilson.Companies.Data.Configurations
{
    public class ProjectLocationTypeConfiguration : EntityTypeConfiguration<ProjectLocation>
    {
        public ProjectLocationTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<ProjectLocation>());
        }

        public override void Map(EntityTypeBuilder<ProjectLocation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.Country).HasMaxLength(70);
            builder.Property(x => x.City).HasMaxLength(70).IsRequired();
            builder.Property(x => x.Street).HasMaxLength(70);
            builder.Property(x => x.Note).HasMaxLength(250);
        }
    }
}
