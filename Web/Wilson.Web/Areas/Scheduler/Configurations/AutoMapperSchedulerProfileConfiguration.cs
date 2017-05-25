using AutoMapper;
using Wilson.Scheduler.Core.Entities;
using Wilson.Web.Areas.Scheduler.Models.HomeViewModels;
using Wilson.Web.Areas.Scheduler.Models.PayrollViewModels;
using Wilson.Web.Areas.Scheduler.Models.SharedViewModels;

namespace Wilson.Web.Areas.Scheduler.Configurations
{
    public class AutoMapperSchedulerProfileConfiguration : Profile
    {
        public AutoMapperSchedulerProfileConfiguration()
        {
            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<Employee, EmployeeConciseViewModel>();
            CreateMap<Project, ProjectViewModel>();
            CreateMap<Schedule, ScheduleViewModel>()
                .ForMember(x => x.ProjectOptions, opt => opt.Ignore())
                .ForMember(x => x.ScheduleOptions, opt => opt.Ignore())
                .ForMember(x => x.ScheduleOptionName, opt => opt.Ignore());
            CreateMap<Paycheck, PaycheckViewModel>();
        }
    }
}
