using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Companies.Core.Entities;

namespace Wilson.Companies.Data.Configurations
{
    public class ApplicationUserTypeConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<ApplicationUser>());
        }

        public override void Map(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(70).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(70).IsRequired();
            builder.Property(x => x.EmployeeId).HasMaxLength(36);
            builder.Property(x => x.IsActive).HasDefaultValue(true);
        }
    }
}
