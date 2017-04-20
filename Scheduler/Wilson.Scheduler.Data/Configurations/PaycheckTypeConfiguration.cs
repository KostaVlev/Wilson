using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Scheduler.Core.Entities;
using Wilson.Scheduler.Data.Extensions;

namespace Wilson.Scheduler.Data.Configurations
{
    public class PaycheckTypeConfiguration : EntityTypeConfiguration<Paycheck>
    {
        public PaycheckTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<Paycheck>());
        }

        public override void Map(EntityTypeBuilder<Paycheck> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.EmployeeId).HasMaxLength(36).IsRequired();
            builder.Property(x => x.PayForHours).HasPrecision(18, 4);
            builder.Property(x => x.PayForExtraHours).HasPrecision(18, 4);
            builder.Property(x => x.PayForHolidayHours).HasPrecision(18, 4);
            builder.Property(x => x.PayForPayedDaysOff).HasPrecision(18, 4);
            builder.Property(x => x.PayBusinessTrip).HasPrecision(18, 4);
            builder.Property(x => x.Total).HasPrecision(18, 4);
        }
    }
}
