using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Projects.Core.Entities;

namespace Wilson.Projects.Data.Configurations
{
    public class EmployeeTypeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<Employee>());
        }

        public override void Map(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.FirstName).HasMaxLength(70).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(70).IsRequired();
            builder.HasMany(x => x.Projects).WithOne(x => x.Manager).HasForeignKey(x => x.ManagerId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
