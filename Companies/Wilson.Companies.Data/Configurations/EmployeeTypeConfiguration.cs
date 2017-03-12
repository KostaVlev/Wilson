using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wilson.Companies.Core.Entities;

namespace Wilson.Companies.Data.Configurations
{
    public class EmployeeTypeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeTypeConfiguration(ModelBuilder modelBuilder)
        {
            this.Map(modelBuilder.Entity<Employee>());
        }

        public override void Map(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirstName).HasMaxLength(70).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(70).IsRequired();
            builder.Property(x => x.Phone).HasMaxLength(15).IsRequired();
            builder.Property(x => x.PrivatePhone).HasMaxLength(15);
            builder.Property(x => x.Email).HasMaxLength(70).IsRequired();
            builder.HasOne(x => x.Company).WithMany(x => x.Employees).HasForeignKey(x => x.CompanyId);
            builder.HasOne(x => x.Address).WithMany().HasForeignKey(x => x.AddressId);
        }
    }
}
