using AutoMapper;
using Wilson.Companies.Core.Entities;
using Wilson.Web.Areas.Admin.Models.ControlPanelViewModels;

namespace Wilson.Web.Areas.Admin.Configurations
{
    public class AutoMapperAdminProfileConfiguration : Profile
    {
        public AutoMapperAdminProfileConfiguration()
        {
            // Admin area mappings.
            CreateMap<User, UserViewModel>();
        }
    }
}
