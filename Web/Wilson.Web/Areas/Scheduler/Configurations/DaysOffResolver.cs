using AutoMapper;
using Wilson.Scheduler.Core.Entities.ValueObjects;
using Wilson.Web.Configurations;

namespace Wilson.Web.Areas.Scheduler.Configurations
{
    public class DaysOffResolver<TSource, TDestination> : IValueResolver<TSource, TDestination, DaysOff>
    {
        public DaysOff Resolve(TSource source, TDestination destination, DaysOff destMember, ResolutionContext context)
        {
            return ProperyResolverFactory.Resolve<TSource, TDestination, DaysOff>(source, destination, "DaysOff") as DaysOff;
        }
    }
}
