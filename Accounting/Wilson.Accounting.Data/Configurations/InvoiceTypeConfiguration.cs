using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Accounting.Core.Entities;
using Wilson.Accounting.Data.Extensions;

namespace Wilson.Accounting.Data.Configurations
{
    public class InvoiceTypeConfiguration : EntityTypeConfiguration<Invoice>
    {
        public InvoiceTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<Invoice>());
        }

        public override void Map(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(p => p.Number).HasMaxLength(10).IsRequired();
            builder.Property(p => p.SubTotal).HasPrecision(18, 4);
            builder.Property(p => p.VatAmount).HasPrecision(18, 4);
            builder.Property(p => p.Total).HasPrecision(18, 4);
            builder.HasOne(x => x.Buyer).WithMany(x => x.BuyInvoices).HasForeignKey(x => x.BuyerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Seller).WithMany(x => x.SaleInvoices).HasForeignKey(x => x.SellerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Bill).WithOne(x => x.Invoice).HasForeignKey<Invoice>(x => x.BillId);
        }
    }
}
