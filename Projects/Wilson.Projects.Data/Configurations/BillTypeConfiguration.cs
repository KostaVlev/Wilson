using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Projects.Core.Entities;
using Wilson.Projects.Data.Extensions;

namespace Wilson.Projects.Data.Configurations
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
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.ProjectId).HasMaxLength(36).IsRequired();
            builder.Property(x => x.CreatedById).HasMaxLength(36).IsRequired();
            builder.Property(x => x.Amount).HasPrecision(18, 4).IsRequired();
            builder.HasOne(x => x.Project).WithMany(x => x.Bills).HasForeignKey(x => x.ProjectId);
        }
    }
}
