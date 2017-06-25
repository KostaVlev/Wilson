using AutoMapper;
using Wilson.Accounting.Core.Entities.ValueObjects;
using Wilson.Web.Configurations;

namespace Wilson.Web.Areas.Accounting.Configurations
{
    public class PeriodResolver<TSource, TDestination> : IValueResolver<TSource, TDestination, Period>
    {
        public Period Resolve(TSource source, TDestination destination, Period destMember, ResolutionContext context)
        {
            return ProperyResolverFactory.Resolve<TSource, TDestination, Period>(source, destination, "Period") as Period;
        }
    }
}
