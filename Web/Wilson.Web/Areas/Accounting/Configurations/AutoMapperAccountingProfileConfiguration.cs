using AutoMapper;
using Wilson.Accounting.Core.Entities;
using Wilson.Web.Areas.Accounting.Models.HomeViewModels;
using Wilson.Web.Areas.Accounting.Models.SharedViewModels;

namespace Wilson.Web.Areas.Accounting.Configurations
{
    public class AutoMapperAccountingProfileConfiguration : Profile
    {
        public AutoMapperAccountingProfileConfiguration()
        {
            CreateMap<Employee, EmployeeViewModel>();
            CreateMap<Payment, PaymentViewModel>();
            CreateMap<Paycheck, PaycheckViewModel>()
                .ForMember(x => x.Payments, opt => opt.ResolveUsing<PaychekPaymentsResolver>());
        }
    }
}
