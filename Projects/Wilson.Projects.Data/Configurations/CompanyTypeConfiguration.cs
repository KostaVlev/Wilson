using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Projects.Core.Entities;

namespace Wilson.Projects.Data.Configurations
{
    public class CompanyTypeConfiguration : EntityTypeConfiguration<Company>
    {
        public CompanyTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<Company>());
        }

        public override void Map(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.Name).HasMaxLength(70).IsRequired();
        }
    }
}
