using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Scheduler.Core.Entities;
using Wilson.Scheduler.Data.Extensions;

namespace Wilson.Scheduler.Data.Configurations
{
    public class PayRateTypeConfiguration : EntityTypeConfiguration<PayRate>
    {
        public PayRateTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<PayRate>());
        }

        public override void Map(EntityTypeBuilder<PayRate> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.Hour).HasPrecision(18, 4);
            builder.Property(x => x.ExtraHour).HasPrecision(18, 4);
            builder.Property(x => x.HoidayHour).HasPrecision(18, 4);
            builder.Property(x => x.BusinessTripHour).HasPrecision(18, 4);
        }
    }
}
