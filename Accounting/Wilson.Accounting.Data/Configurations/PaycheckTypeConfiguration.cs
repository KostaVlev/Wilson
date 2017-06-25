using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Accounting.Core.Entities;
using Wilson.Accounting.Data.Extensions;

namespace Wilson.Accounting.Data.Configurations
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
            builder.Property(x => x.Period).HasMaxLength(80).IsRequired();
            builder.Property(x => x.SubTotals).HasMaxLength(250).IsRequired();
            builder.Property(x => x.DaysOff).HasMaxLength(250).IsRequired();
            builder.Property(x => x.WorkingHours).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Total).HasPrecision(18, 4);
        }
    }
}
