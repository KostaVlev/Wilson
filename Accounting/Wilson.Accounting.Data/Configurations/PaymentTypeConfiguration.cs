using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Accounting.Core.Entities;

namespace Wilson.Accounting.Data.Configurations
{
    public class PaymentTypeConfiguration : EntityTypeConfiguration<Payment>
    {
        public PaymentTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<Payment>());
        }

        public override void Map(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.InvoiceId).IsRequired();
            builder.Property(x => x.PriceId).IsRequired();            
            builder.HasOne(x => x.Invoice).WithMany(x => x.Payments).HasForeignKey(x => x.InvoiceId);
            builder.HasOne(x => x.Price).WithOne(x => x.Payment).HasForeignKey<Payment>(x => x.PriceId);
        }
    }
}
