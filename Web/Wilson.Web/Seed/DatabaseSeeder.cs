using Microsoft.Extensions.DependencyInjection;
using Wilson.Web.Events.Interfaces;

namespace Wilson.Web.Seed
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        public void Seed(IServiceScopeFactory services, IEventsFactory eventsFactory)
        {
            AccountingDbSeeder.Seed(services, eventsFactory);
            CompaniesDbSeeder.Seed(services, eventsFactory);
            ProjectsDbSeeder.Seed(services, eventsFactory);            
            SchedulerDbSeeder.Seed(services, eventsFactory);
        }
    }
}
