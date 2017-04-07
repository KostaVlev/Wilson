using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Companies.Core.Entities;

namespace Wilson.Companies.Data.Configurations
{
    public class MessageTypeConfiguration : EntityTypeConfiguration<Message>
    {
        public MessageTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<Message>());
        }

        public override void Map(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasMaxLength(36);
            builder.Property(x => x.Subject).HasMaxLength(70).IsRequired();
            builder.Property(x => x.Body).HasMaxLength(900).IsRequired();
            builder.Property(x => x.IsNew).HasDefaultValue(true);
            builder.HasOne(x => x.Sender).WithMany(x => x.SentMessages).HasForeignKey(x => x.SenderId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Recipient).WithMany(x => x.ReceivedMessages).HasForeignKey(x => x.RecipientId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
