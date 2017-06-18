using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using Wilson.Accounting.Data.DataAccess;
using Wilson.Web.Areas.Accounting.Models.PayrollViewModels;
using Wilson.Web.Areas.Accounting.Services;
using Wilson.Web.Events.Interfaces;

namespace Wilson.Web.Areas.Accounting.ViewComponents
{
    public class AddPaycheckPaymentViewComponent : BaseViewComponent
    {
        public AddPaycheckPaymentViewComponent(
            IAccountingWorkData accountingWorkData, 
            IPayrollService payrollService, 
            IMapper mapper, 
            IEventsFactory eventsFactory) 
            : base(accountingWorkData, payrollService, mapper, eventsFactory)
        {
        }

        public IViewComponentResult Invoke(DateTime from, DateTime to, string employeeId, string paycheckId)
        {
            return View(AddPaymentViewModel.Create(from, to, employeeId, paycheckId));
        }
    }
}
