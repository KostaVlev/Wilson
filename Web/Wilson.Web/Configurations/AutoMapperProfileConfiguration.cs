using AutoMapper;
using Wilson.Companies.Core.Entities;
using Wilson.Web.Areas.Admin.Models.ControlPanelViewModels;

namespace Wilson.Web.Configurations
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<User, UserViewModel>();
        }
    }
}
