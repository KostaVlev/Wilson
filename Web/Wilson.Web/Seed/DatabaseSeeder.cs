using Microsoft.Extensions.DependencyInjection;

namespace Wilson.Web.Seed
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        public void Seed(IServiceScopeFactory services)
        {
            AccountingDbSeeder.Seed(services);
            CompaniesDbSeeder.Seed(services);
        }
    }
}
