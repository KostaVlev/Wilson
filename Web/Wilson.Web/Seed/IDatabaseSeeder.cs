using Microsoft.Extensions.DependencyInjection;
using Wilson.Web.Events.Interfaces;

namespace Wilson.Web.Seed
{
    public interface IDatabaseSeeder
    {
        void Seed(IServiceScopeFactory services, IEventsFactory eventsFactory);
    }
}
