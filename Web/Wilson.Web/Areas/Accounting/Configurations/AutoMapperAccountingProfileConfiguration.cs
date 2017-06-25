using AutoMapper;
using Wilson.Accounting.Core.Entities;
using Wilson.Accounting.Core.Entities.ValueObjects;
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
                .ForMember(x => x.Payments, opt => opt.ResolveUsing<PaychekPaymentsResolver>())
                .ForMember(x => x.Period, opt => opt.ResolveUsing<PeriodResolver<Paycheck, PaycheckViewModel>>());
        }
    }
}
