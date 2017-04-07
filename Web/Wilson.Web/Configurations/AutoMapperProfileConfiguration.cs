using AutoMapper;
using Wilson.Companies.Core.Entities;
using Wilson.Web.Models.SharedViewModels;

namespace Wilson.Web.Configurations
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<UserViewModel, User>();
            CreateMap<CompanyViewModel, Company>();
            CreateMap<AddressViewModel, Address>();
        }
    }
}
