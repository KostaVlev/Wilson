using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Wilson.Accounting.Data.DataAccess;
using Wilson.Web.Areas.Accounting.Models.HomeViewModels;
using Wilson.Web.Areas.Accounting.Services;
using Wilson.Web.Events.Interfaces;

namespace Wilson.Web.Areas.Accounting.Controllers
{
    public class HomeController : AccountingBaseController
    {
        public HomeController(
            IAccountingWorkData accountingWorkData, 
            IPayrollService payrollService, 
            IMapper mapper, 
            IEventsFactory eventsFactory)
            : base(accountingWorkData, payrollService, mapper, eventsFactory)
        {
        }

        //
        // GET: /Accounting/Home
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //
        // GET: /Accounting/Home/Payroll
        [HttpGet]
        public async Task<IActionResult> Payroll(string errorMessage, DateTime from, DateTime to, string employeeId)
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ModelState.AddModelError(string.Empty, errorMessage);
                return View(await PayrollViewModel.CreateAsync(from, to, employeeId, this.PayrollService, this.Mapper));
            }

            return View(await PayrollViewModel.CreateAsync(this.PayrollService));
        }

        //
        // POST: /Accounting/Home/Payroll
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payroll(PayrollViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(await PayrollViewModel.ReBuildAsync(model, this.PayrollService));
            }

            return View(await PayrollViewModel.CreateAsync(model.From, model.To, model.EmployeeId, this.PayrollService, this.Mapper));
        }
    }
}
