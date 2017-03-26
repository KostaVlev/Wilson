using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Wilson.Web.Database
{
    public static class DatabaseSeeder
    {
        public static void Seed(IWebHost host)
        {
            var services = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));
            AccountingDbSeeder.Seed(services);
        }
    }
}
