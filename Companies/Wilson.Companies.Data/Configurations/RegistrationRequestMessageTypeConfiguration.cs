using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Companies.Core.Entities;

namespace Wilson.Companies.Data.Configurations
{
    public class RegistrationRequestMessageTypeConfiguration : EntityTypeConfiguration<RegistrationRequestMessage>
    {
        public RegistrationRequestMessageTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<RegistrationRequestMessage>());
        }

        public override void Map(EntityTypeBuilder<RegistrationRequestMessage> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(70).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(70).IsRequired();
            builder.Property(x => x.IsNew).HasDefaultValue(true);
            builder.HasOne(x => x.Recipient).WithMany(x => x.RegistrationRequestMessages).HasForeignKey(x => x.RecipientId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
