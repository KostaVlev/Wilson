using AutoMapper;
using Wilson.Companies.Core.Entities;
using Wilson.Web.Areas.Companies.Models.InquiriesViewModels;
using Wilson.Web.Areas.Companies.Models.SharedViewModels;

namespace Wilson.Web.Areas.Companies.Configurations
{
    public class AutoMapperCompaniesProfileConfiguration : Profile
    {
        public AutoMapperCompaniesProfileConfiguration()
        {
            // Companies area mappings.
            CreateMap<Inquiry, InquiryViewModel>();
            CreateMap<CreateViewModel, Inquiry>().ForMember(x => x.Attachmnets, opt => opt.Ignore());
            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<InquiryEmployee, InquiryEmployeeViewModel>();
            CreateMap<Company, CompanyViewModel>();
            CreateMap<Attachment, AttachmentViewModel>();
            CreateMap<InfoRequest, InfoRequestViewModel>();
            CreateMap<Offer, OfferViewModel>();
        }
    }
}
