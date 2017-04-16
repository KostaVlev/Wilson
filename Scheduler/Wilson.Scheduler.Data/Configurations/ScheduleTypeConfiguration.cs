using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Scheduler.Core.Entities;

namespace Wilson.Scheduler.Data.Configurations
{
    public class ScheduleTypeConfiguration : EntityTypeConfiguration<Schedule>
    {
        public ScheduleTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<Schedule>());
        }

        public override void Map(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.ProjectId).HasMaxLength(36);
            builder.Property(x => x.EmployeeId).HasMaxLength(36).IsRequired();
            builder.HasOne(x => x.Project).WithMany().HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
