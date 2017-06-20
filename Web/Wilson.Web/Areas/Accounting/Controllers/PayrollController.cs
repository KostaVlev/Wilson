using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wilson.Accounting.Data.DataAccess;
using Wilson.Web.Areas.Accounting.Models.PayrollViewModels;
using Wilson.Web.Areas.Accounting.Services;
using Wilson.Web.Events.Interfaces;
using System.Collections.Generic;

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
                var errorMessage = ViewData.ModelState.Values.FirstOrDefault(v => v.Errors.Any()).Errors.FirstOrDefault().ErrorMessage;
                return RedirectToHomePayrollWithMessage(errorMessage, employeeId: model.EmployeeId);
            }

            var paycheck = await this.PayrollService.FindEmployeePaycheck(model.EmployeeId, model.PaycheckId);
            if (paycheck == null)
            {
                return RedirectToHomePayrollWithMessage("Paycheck not found.");
            }

            if (paycheck.Total < paycheck.GetPaidAmount() + model.Payment.Amount)
            {
                var errorMessage = string.Format("Maximum amount that can be payed is {0}", paycheck.Total - paycheck.GetPaidAmount());
                return RedirectToHomePayrollWithMessage(errorMessage, employeeId: model.EmployeeId);
            }

            if (model.Payment.Amount < 0)
            {
                return RedirectToHomePayrollWithMessage("Amount can't be negative number.", employeeId: model.EmployeeId);
            }

            await this.PayrollService.AddPayment(paycheck, DateTime.Today, model.Payment.Amount);

            return RedirectToAction(nameof(HomeController.Payroll), "Home");
        }
        
        //AJAX: /Accounting/Payroll/AddPaycheckPayment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPaycheckPayments(IEnumerable<AddPaymentViewModel> model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ViewData.ModelState.Values.FirstOrDefault(v => v.Errors.Any()).Errors.FirstOrDefault().ErrorMessage;
                return RedirectToHomePayrollWithMessage(errorMessage);
            }

            var paycheckIds = model.Select(m => m.PaycheckId);
            var paychecks = await this.PayrollService.FindEmployeePaychecks(paycheckIds);
            if (paychecks == null || paychecks.Count() <= 0)
            {
                return RedirectToHomePayrollWithMessage("Paycheck not found.");
            }
            
            var paycheckAmountPair = model.Select(m =>
                        new { PaycheckId = m.PaycheckId, Amount = m.Payment.Amount }).ToDictionary(k => k.PaycheckId, v => v.Amount);

            if (paychecks.Any(p => p.Total < p.GetPaidAmount() - paycheckAmountPair[p.Id]))
            {
                var paycheck = paychecks.FirstOrDefault(p => p.Total < p.GetPaidAmount() - paycheckAmountPair[p.Id]);
                var errorMessage = string.Format("Maximum amount that can be payed is {0}", paycheck.Total - paycheck.GetPaidAmount());
                return RedirectToHomePayrollWithMessage(errorMessage, employeeId: paycheck.EmployeeId);
            }

            if (model.Select(m => m.Payment.Amount).Any(a => a < 0))
            {
                return RedirectToHomePayrollWithMessage("Only positive amount numbers are allowed.");
            }
            
            await this.PayrollService.AddPayments(paychecks, DateTime.Today, paycheckAmountPair);


            return RedirectToHomePayrollWithMessage("Payments have been updated.");
        }

        private RedirectToActionResult RedirectToHomePayrollWithMessage(
            string errorMessage, DateTime? from = null, DateTime? to = null, string employeeId = null)
        {
            return RedirectToAction(
                    nameof(HomeController.Payroll),
                    "Home",
                    new
                    {
                        ErrorMessage = errorMessage,
                        From = from ?? DateTime.Today.AddMonths(-1),
                        To = to ?? DateTime.Today,
                        EmployeeId = employeeId ?? string.Empty
                    });
        }
    }
}
