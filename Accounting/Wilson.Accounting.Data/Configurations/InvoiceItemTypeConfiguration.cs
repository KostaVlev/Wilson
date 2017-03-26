using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Accounting.Core.Entities;

namespace Wilson.Accounting.Data.Configurations
{
    public class InvoiceItemTypeConfiguration : EntityTypeConfiguration<InvoiceItem>
    {
        public InvoiceItemTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<InvoiceItem>());
        }

        public override void Map(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.HasKey(x => new { x.InvoiceId, x.ItemId, x.PriceId });
            builder.Property(x => x.InvoiceId).HasMaxLength(36);
            builder.Property(x => x.PriceId).HasMaxLength(36);
            builder.HasOne(x => x.Invoice).WithMany(x => x.Items).HasForeignKey(x => x.InvoiceId);
            builder.HasOne(x => x.Item).WithMany(x => x.Invoices).HasForeignKey(x => x.ItemId);
            builder.HasOne(x => x.Price).WithMany().HasForeignKey(x => x.PriceId);
        }
    }
}
