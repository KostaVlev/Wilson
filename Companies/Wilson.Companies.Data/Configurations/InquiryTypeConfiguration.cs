using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Companies.Core.Entities;

namespace Wilson.Companies.Data.Configurations
{
    public class InquiryTypeConfiguration : EntityTypeConfiguration<Inquiry>
    {
        public InquiryTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<Inquiry>());
        }

        public override void Map(EntityTypeBuilder<Inquiry> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Description).HasMaxLength(900).IsRequired();
            builder.HasOne(x => x.RecivedBy).WithMany().HasForeignKey(x => x.ReceivedById).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Customer).WithMany().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Project).WithMany().HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Attachmnets).WithOne(x => x.Inquiry).HasForeignKey(x => x.InquiryId);
            builder.HasMany(x => x.InfoRequests).WithOne(x => x.Inquiry).HasForeignKey(x => x.InquiryId);
        }
    }
}
