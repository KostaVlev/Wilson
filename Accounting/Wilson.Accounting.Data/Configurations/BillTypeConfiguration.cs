using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Accounting.Core.Entities;
using Wilson.Accounting.Data.Extensions;

namespace Wilson.Accounting.Data.Configurations
{
    public class BillTypeConfiguration : EntityTypeConfiguration<Bill>
    {
        public BillTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<Bill>());
        }

        public override void Map(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Amount).HasPrecision(18, 4).IsRequired();
            builder.HasOne(x => x.Project).WithMany(x => x.Bills).HasForeignKey(x => x.ProjectId);
            builder.HasOne(x => x.Invoice).WithOne(x => x.Bill).HasForeignKey<Bill>(x => x.InvoiceId);
        }
    }
}
