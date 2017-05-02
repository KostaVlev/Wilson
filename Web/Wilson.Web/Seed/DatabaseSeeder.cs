using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Wilson.Web.Seed
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        public void Seed(IServiceScopeFactory services, IMapper mapper)
        {
            AccountingDbSeeder.Seed(services, mapper);
            CompaniesDbSeeder.Seed(services);
        }
    }
}
