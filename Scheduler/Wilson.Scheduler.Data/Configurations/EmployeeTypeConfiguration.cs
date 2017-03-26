using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Scheduler.Core.Entities;

namespace Wilson.Scheduler.Data.Configurations
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
            builder.Property(x => x.PayRateId).HasMaxLength(36).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(70).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(70).IsRequired();
            builder.Property(x => x.EmployeePosition).IsRequired();
            builder.HasOne(x => x.PayRate).WithMany().HasForeignKey(x => x.PayRateId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Schedules).WithOne(x => x.Employee).HasForeignKey(x => x.EmployeeId);
            builder.HasMany(x => x.Paychecks).WithOne(x => x.Employee).HasForeignKey(x => x.EmployeeId);
        }
    }
}
