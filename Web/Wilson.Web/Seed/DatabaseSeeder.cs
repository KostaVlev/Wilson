using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Wilson.Web.Seed
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        public void Seed(IServiceScopeFactory services)
        {
            CompaniesDbSeeder.Seed(services);
            ProjectsDbSeeder.Seed(services);
            AccountingDbSeeder.Seed(services);
            SchedulerDbSeeder.Seed(services);
        }
    }
}
