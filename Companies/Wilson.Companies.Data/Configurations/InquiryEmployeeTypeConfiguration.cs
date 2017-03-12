using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Companies.Core.Entities;

namespace Wilson.Companies.Data.Configurations
{
    public class InquiryEmployeeTypeConfiguration : EntityTypeConfiguration<InquiryEmployee>
    {
        public InquiryEmployeeTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<InquiryEmployee>());
        }

        public override void Map(EntityTypeBuilder<InquiryEmployee> builder)
        {
            builder.HasKey(x => new { x.InquiryId, x.EmployeeId });
            builder.HasOne(x => x.Employee).WithMany().HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Inquiry).WithMany(x => x.Assignees).HasForeignKey(x => x.InquiryId);
        }
    }
}
