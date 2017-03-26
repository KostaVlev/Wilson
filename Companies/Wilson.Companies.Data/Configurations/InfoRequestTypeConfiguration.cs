using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Companies.Core.Entities;

namespace Wilson.Companies.Data.Configurations
{
    public class InfoRequestTypeConfiguration : EntityTypeConfiguration<InfoRequest>
    {
        public InfoRequestTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<InfoRequest>());
        }


        public override void Map(EntityTypeBuilder<InfoRequest> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.InquiryId).HasMaxLength(36).IsRequired();
            builder.Property(x => x.SentById).HasMaxLength(36).IsRequired();
            builder.Property(x => x.RequestMessage).HasMaxLength(900).IsRequired();
            builder.Property(x => x.ResponseMessage).HasMaxLength(900);
            builder.HasOne(x => x.Inquiry).WithMany(x => x.InfoRequests).HasForeignKey(x => x.InquiryId);
            builder.HasOne(x => x.SentBy).WithMany(x => x.InfoRequests).HasForeignKey(x => x.SentById);
            builder.HasMany(x => x.RequestAttachmnets).WithOne(x => x.InfoRequest).HasForeignKey(x => x.InfoRequestId);
            builder.HasMany(x => x.ResponseAttachmnets).WithOne(x => x.InforequestResponse).HasForeignKey(x => x.InforequestResponseId);
        }
    }
}
