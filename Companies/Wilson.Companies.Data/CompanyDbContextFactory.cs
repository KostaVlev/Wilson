using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Wilson.Companies.Data
{
    public class CompanyDbContextFactory : IDbContextFactory<CompanyDbContext>
    {
        public CompanyDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<CompanyDbContext>();
            builder.UseSqlServer("Server=.;Database=Wilson;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new CompanyDbContext(builder.Options);
        }
    }
}
