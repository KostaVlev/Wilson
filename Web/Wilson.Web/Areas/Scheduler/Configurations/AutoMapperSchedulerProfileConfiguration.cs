using AutoMapper;
using Wilson.Scheduler.Core.Entities;
using Wilson.Web.Areas.Scheduler.Models.SharedViewModels;

namespace Wilson.Web.Areas.Scheduler.Configurations
{
    public class AutoMapperSchedulerProfileConfiguration : Profile
    {
        public AutoMapperSchedulerProfileConfiguration()
        {
            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<Project, ProjectViewModel>();
            CreateMap<Schedule, ScheduleViewModel>();            
        }
    }
}
