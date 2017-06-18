using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wilson.Accounting.Data.DataAccess;
using Wilson.Web.Areas.Accounting.Models.PayrollViewModels;
using Wilson.Web.Areas.Accounting.Services;
using Wilson.Web.Events.Interfaces;

namespace Wilson.Web.Areas.Accounting.Controllers
{
    public class PayrollController : AccountingBaseController
    {
        public PayrollController(
            IAccountingWorkData accountingWorkData,
            IPayrollService payrollService,
            IMapper mapper,
            IEventsFactory eventsFactory)
            : base(accountingWorkData, payrollService, mapper, eventsFactory)
        {
        }


        //POST: /Accounting/Payroll/AddPaycheckPayment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPaycheckPayment(AddPaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(
                    nameof(HomeController.Payroll),
                    "Home",
                    new
                    {
                        ErrorMessage = ViewData.ModelState.Values.FirstOrDefault(v => v.Errors.Any()).Errors.FirstOrDefault().ErrorMessage,
                        From = model.From,
                        To = model.To,
                        EmployeeId = model.EmployeeId
                    });
            }

            var paycheck = await this.PayrollService.FindEmployeePaycheck(model.EmployeeId, model.PaycheckId);
            if (paycheck == null)
            {
                ModelState.AddModelError(string.Empty, "Paycheck not found.");

                return RedirectToAction(
                    nameof(HomeController.Payroll),
                    "Home",
                    new
                    {
                        ErrorMessage = "Paycheck not found.",
                        From = model.From,
                        To = model.To,
                        EmployeeId = model.EmployeeId
                    });
            }

            if (paycheck.Total < paycheck.GetPaidAmount() + model.Payment.Amount)
            {
                return RedirectToAction(
                    nameof(HomeController.Payroll),
                    "Home",
                    new
                    {
                        ErrorMessage = string.Format("Maximum amount that can be payed is {0}", paycheck.Total - paycheck.GetPaidAmount()),
                        From = model.From,
                        To = model.To,
                        EmployeeId = model.EmployeeId
                    });
            }

            if (model.Payment.Amount < 0)
            {
                return RedirectToAction(
                    nameof(HomeController.Payroll),
                    "Home",
                    new
                    {
                        ErrorMessage = "Amount can't be negative number.",
                        From = model.From,
                        To = model.To,
                        EmployeeId = model.EmployeeId
                    });
            }

            await this.PayrollService.AddPayment(paycheck, DateTime.Today, model.Payment.Amount);

            return RedirectToAction(nameof(HomeController.Payroll), "Home");
        }
    }
}
