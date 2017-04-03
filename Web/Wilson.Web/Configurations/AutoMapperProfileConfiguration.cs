using AutoMapper;
using Wilson.Companies.Core.Entities;
using Wilson.Web.Models.InstallViewModels;

namespace Wilson.Web.Configurations
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<InstallDatabaseViewModel, User>()
                .ForSourceMember(x => x.Name, opt => opt.Ignore())
                .ForSourceMember(x => x.RegistrationNumber, opt => opt.Ignore())
                .ForSourceMember(x => x.VatNumber, opt => opt.Ignore())
                .ForSourceMember(x => x.OfficeEmail, opt => opt.Ignore())
                .ForSourceMember(x => x.OfficePhone, opt => opt.Ignore())
                .ForSourceMember(x => x.Country, opt => opt.Ignore())
                .ForSourceMember(x => x.Street, opt => opt.Ignore())
                .ForSourceMember(x => x.StreetNumber, opt => opt.Ignore())
                .ForSourceMember(x => x.UnitNumber, opt => opt.Ignore())
                .ForSourceMember(x => x.PostCode, opt => opt.Ignore())
                .ForSourceMember(x => x.Floor, opt => opt.Ignore())
                .ForSourceMember(x => x.SeedData, opt => opt.Ignore());
            CreateMap<InstallDatabaseViewModel, Company>()
                .ForSourceMember(x => x.FirstName, opt => opt.Ignore())
                .ForSourceMember(x => x.LastName, opt => opt.Ignore())
                .ForSourceMember(x => x.Email, opt => opt.Ignore())
                .ForSourceMember(x => x.Password, opt => opt.Ignore())
                .ForSourceMember(x => x.ConfirmPassword, opt => opt.Ignore())
                .ForSourceMember(x => x.Country, opt => opt.Ignore())
                .ForSourceMember(x => x.Street, opt => opt.Ignore())
                .ForSourceMember(x => x.StreetNumber, opt => opt.Ignore())
                .ForSourceMember(x => x.UnitNumber, opt => opt.Ignore())
                .ForSourceMember(x => x.PostCode, opt => opt.Ignore())
                .ForSourceMember(x => x.Floor, opt => opt.Ignore())
                .ForSourceMember(x => x.SeedData, opt => opt.Ignore());
            CreateMap<InstallDatabaseViewModel, Address>()
                .ForSourceMember(x => x.FirstName, opt => opt.Ignore())
                .ForSourceMember(x => x.LastName, opt => opt.Ignore())
                .ForSourceMember(x => x.Email, opt => opt.Ignore())
                .ForSourceMember(x => x.Password, opt => opt.Ignore())
                .ForSourceMember(x => x.ConfirmPassword, opt => opt.Ignore())
                .ForSourceMember(x => x.Name, opt => opt.Ignore())
                .ForSourceMember(x => x.RegistrationNumber, opt => opt.Ignore())
                .ForSourceMember(x => x.VatNumber, opt => opt.Ignore())
                .ForSourceMember(x => x.OfficeEmail, opt => opt.Ignore())
                .ForSourceMember(x => x.OfficePhone, opt => opt.Ignore())
                .ForSourceMember(x => x.SeedData, opt => opt.Ignore());
        }
    }
}
