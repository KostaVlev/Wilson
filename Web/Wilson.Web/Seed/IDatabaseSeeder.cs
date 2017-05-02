using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Wilson.Web.Seed
{
    public interface IDatabaseSeeder
    {
        void Seed(IServiceScopeFactory services);
    }
}
