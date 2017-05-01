using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Accounting.Core.Entities;
using Wilson.Accounting.Data.Extensions;

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
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36).IsRequired();
            builder.Property(x => x.InvoiceId).HasMaxLength(36).IsRequired();
            builder.Property(x => x.ItemId).HasMaxLength(36).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Price).HasMaxLength(36).HasPrecision(18, 4).IsRequired();
            builder.HasOne(x => x.Invoice).WithMany(x => x.InvoiceItems).HasForeignKey(x => x.InvoiceId);
            builder.HasOne(x => x.Item).WithMany(x => x.InvoiceItems).HasForeignKey(x => x.ItemId);
        }
    }
}
