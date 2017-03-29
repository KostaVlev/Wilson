using Microsoft.Extensions.DependencyInjection;

namespace Wilson.Web.Seed
{
    public interface IRolesSeder
    {
        void Seed(IServiceScopeFactory services);
    }
}
