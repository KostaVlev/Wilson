using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Companies.Core.Entities;

namespace Wilson.Companies.Data.Configurations
{
    public class CompanyContractTypeConfiguration : EntityTypeConfiguration<CompanyContract>
    {
        public CompanyContractTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<CompanyContract>());
        }

        public override void Map(EntityTypeBuilder<CompanyContract> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.ProjectId).HasMaxLength(36).IsRequired();
            builder.Property(x => x.CretedById).HasMaxLength(36).IsRequired();
            builder.HasMany(x => x.Offers).WithOne(x => x.Contract).HasForeignKey(x => x.ContractId);
            builder.HasOne(x => x.Project).WithOne(x => x.Contract).HasForeignKey<CompanyContract>(x => x.ProjectId);
            builder.HasMany(x => x.Attachments).WithOne(x => x.Cotract).HasForeignKey(x => x.ContractId);
            builder.HasOne(x => x.CretedBy).WithMany().HasForeignKey(x => x.CretedById).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
