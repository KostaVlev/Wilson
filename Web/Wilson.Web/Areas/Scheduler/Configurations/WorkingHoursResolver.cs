using AutoMapper;
using Wilson.Scheduler.Core.Entities.ValueObjects;
using Wilson.Web.Configurations;

namespace Wilson.Web.Areas.Scheduler.Configurations
{
    public class WorkingHoursResolver<TSource, TDestination> : IValueResolver<TSource, TDestination, WorkingHours>
    {
        public WorkingHours Resolve(TSource source, TDestination destination, WorkingHours destMember, ResolutionContext context)
        {
            return ProperyResolverFactory.Resolve<TSource, TDestination, WorkingHours>(source, destination, "WorkingHours") as WorkingHours;
        }
    }
}
