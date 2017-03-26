using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Companies.Core.Entities;

namespace Wilson.Companies.Data.Configurations
{
    public class AttachmentTypeConfiguration : EntityTypeConfiguration<Attachment>
    {
        public AttachmentTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<Attachment>());
        }

        public override void Map(EntityTypeBuilder<Attachment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.ContractId).HasMaxLength(36);
            builder.Property(x => x.InfoRequestId).HasMaxLength(36);
            builder.Property(x => x.InforequestResponseId).HasMaxLength(36);
            builder.Property(x => x.ContractId).HasMaxLength(36);
            builder.Property(x => x.FileName).HasMaxLength(70).IsRequired();
            builder.Property(x => x.Extention).HasMaxLength(4).IsRequired();
            builder.Property(x => x.File).IsRequired();
        }
    }
}
