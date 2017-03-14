using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Wilson.Scheduler.Data
{
    public class SchedulerDbContextFactory : IDbContextFactory<SchedulerDbContext>
    {
        public SchedulerDbContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<SchedulerDbContext>();
            builder.UseSqlServer("Server=.;Database=Wilson;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new SchedulerDbContext(builder.Options);
        }
    }
}
