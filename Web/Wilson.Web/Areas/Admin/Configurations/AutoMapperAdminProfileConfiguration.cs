using AutoMapper;
using Wilson.Companies.Core.Entities;
using Wilson.Web.Areas.Admin.Models.ControlPanelViewModels;
using Wilson.Web.Areas.Admin.Models.SharedViewModels;

namespace Wilson.Web.Areas.Admin.Configurations
{
    public class AutoMapperAdminProfileConfiguration : Profile
    {
        public AutoMapperAdminProfileConfiguration()
        {
            // Admin area mappings.
            CreateMap<ApplicationUser, UserViewModel>();
            CreateMap<RegisterUserViewModel, ApplicationUser>()
                .ForSourceMember(x => x.EmployeePosition, opt => opt.Ignore())
                .ForSourceMember(x => x.EmployeePositions, opt => opt.Ignore())
                .ForSourceMember(x => x.Password, opt => opt.Ignore())
                .ForSourceMember(x => x.ConfirmPassword, opt => opt.Ignore())
                .ForSourceMember(x => x.PrivatePhone, opt => opt.Ignore());
            CreateMap<RegisterUserViewModel, Employee>()
                .ForSourceMember(x => x.Password, opt => opt.Ignore())
                .ForSourceMember(x => x.ConfirmPassword, opt => opt.Ignore())
                .ForSourceMember(x => x.EmployeePositions, opt => opt.Ignore());
            CreateMap<AddressViewModel, Address>();
        }
    }
}
