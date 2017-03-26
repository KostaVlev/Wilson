using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Companies.Core.Entities;

namespace Wilson.Companies.Data.Configurations
{
    public class OfferTypeConfiguration : EntityTypeConfiguration<Offer>
    {
        public OfferTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<Offer>());
        }

        public override void Map(EntityTypeBuilder<Offer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.InquiryId).HasMaxLength(36).IsRequired();
            builder.Property(x => x.SentById).HasMaxLength(36).IsRequired();
            builder.Property(x => x.ContractId).HasMaxLength(36);
            builder.Property(x => x.HtmlContent).IsRequired();
            builder.HasOne(x => x.Inquiry).WithMany(x => x.Offers).HasForeignKey(x => x.InquiryId);
            builder.HasOne(x => x.SentBy).WithMany().HasForeignKey(x => x.SentById).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
