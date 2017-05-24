using AutoMapper;
using Wilson.Companies.Core.Entities;
using Wilson.Web.Areas.Admin.Models.ControlPanelViewModels;
using Wilson.Web.Models.SharedViewModels;

namespace Wilson.Web.Areas.Admin.Configurations
{
    public class AutoMapperAdminProfileConfiguration : Profile
    {
        public AutoMapperAdminProfileConfiguration()
        {
            // Admin area mappings.
            CreateMap<ApplicationUser, ShortUserViewModel>();
            CreateMap<EmployeeViewModel, ApplicationUser>()
                .ForSourceMember(x => x.EmployeePosition, opt => opt.Ignore())
                .ForSourceMember(x => x.EmployeePositions, opt => opt.Ignore())
                .ForSourceMember(x => x.PrivatePhone, opt => opt.Ignore());
            CreateMap<EmployeeViewModel, Employee>()
                .ForSourceMember(x => x.EmployeePositions, opt => opt.Ignore());
            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<AddressViewModel, Address>();
        }
    }
}
