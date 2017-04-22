using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Wilson.Scheduler.Data.DataAccess;
using Microsoft.EntityFrameworkCore;
using Wilson.Web.Areas.Scheduler.Models.SharedViewModels;
using Wilson.Scheduler.Core.Entities;
using Wilson.Web.Areas.Scheduler.Services;
using Wilson.Web.Areas.Scheduler.Models.PayrollViewModels;

namespace Wilson.Web.Areas.Scheduler.Controllers
{
    public class PayrollController : SchedulerBaseController
    {
        public PayrollController(ISchedulerWorkData schedulerWorkData, IMapper mapper, IPayrollService payrollService)
            : base(schedulerWorkData, mapper)
        {
            this.PayrollService = payrollService;
            this.PayrollService.SchedulerWorkData = schedulerWorkData;
        }

        public IPayrollService PayrollService { get; set; }

        //
        // GET: Scheduler/Home/Index
        [HttpGet]
        public IActionResult Index(string message)
        {
            ViewData["StatusMessage"] = message ?? "";

            return View();
        }

        //
        // POST: Scheduler/Home/PreparePayrolls
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PreparePayrolls([FromForm]string period)
        {
            DateTime paycheckIssueDate = DateTime.Now;
            DateTime fromDate;
            if (!string.IsNullOrEmpty(period))
            {
                fromDate = this.PayrollService.TryParsePeriod(period);
            }
            else
            {
                fromDate = DateTime.Now;
            }

            var employeesWithoutPaychecks = await this.PayrollService.GetEmployeesWithoutPaycheks(fromDate);

            if (employeesWithoutPaychecks.Count() == 0)
            {
                return RedirectToAction(nameof(PayrollController.Index), new { Message = Constants.PayrollMessages.PayrollsCreted });
            }

            foreach (var employee in employeesWithoutPaychecks)
            {
                employee.AddNewPaycheck(paycheckIssueDate, fromDate);
            }

            var success = await this.SchedulerWorkData.CompleteAsync();
            if (success == 0)
            {
                return RedirectToAction(nameof(PayrollController.Index), new { Message = Constants.ExceptionMessages.DatabaseUpdateError });
            }

            return RedirectToAction(nameof(PayrollController.Index), new { Message = Constants.SuccessMessages.DatabaseUpdateSuccess });
        }

        //
        // GET: Scheduler/Home/ReviewPaychecks
        [HttpGet]
        public async Task<IActionResult> ReviewPaychecks()
        {
            var model = await this.PayrollService.PrepareReviewPaychecksViewModel();

            return View(model);
        }
        //
        // POST: Scheduler/Home/ReviewPaychecks
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReviewPaychecks(ReviewPaychecksViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var modelToReturn = await this.PayrollService.PrepareReviewPaychecksViewModel();

            if (ModelState.IsValid)
            {
                var employeeModels = await this.PayrollService.FindEmployeesPayshecks(model.From, model.To, model.EmployeeId);
                modelToReturn.Employees = employeeModels;
                modelToReturn.From = model.From;
                modelToReturn.To = model.To;
            }
            
            return View(modelToReturn);
        }
    }
}
