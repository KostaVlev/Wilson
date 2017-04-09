using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Wilson.Scheduler.Data.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wilson.Scheduler.Core.Entities;
using Wilson.Web.Areas.Scheduler.Models.SharedViewModels;
using Wilson.Web.Areas.Scheduler.Models.HomeViewModels;

namespace Wilson.Web.Areas.Scheduler.Controllers
{
    public class HomeController : SchedulerBaseController
    {
        public HomeController(ISchedulerWorkData schedulerWorkData, IMapper mapper)
            : base(schedulerWorkData, mapper)
        {
        }

        //
        // GET: Scheduler/Home/Index
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //
        // GET: Scheduler/Home/EmployeesScheduler
        [HttpGet]
        public async Task<IActionResult> EmployeesScheduler()
        {
            // Make Collection of EmployeeViewModel
            var sevenDaysAgo = DateTime.Now.AddDays(-7);
            var employees = await this.SchedulerWorkData.Employees.GetAllAsync();
            var schedules = await this.SchedulerWorkData.Schedules.FindAsync(s => s.Date >= sevenDaysAgo, x => x
            .Include(s => s.Project));
            foreach (var emp in employees)
            {
                emp.Schedules = schedules.Where(x => x.EmployeeId == emp.Id).ToList();
            }

            var employeeModels = this.Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            // Make collection of ProjectViewModels that will be used for drop-down list. Select only active Projects.
            var projects = await this.SchedulerWorkData.Projects.FindAsync(x => x.IsActive);
            var projectModels = this.Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projects);
            
            return View(new EmployeesSchedulerViewModel() { Employees = employeeModels, Projects = projectModels });
        }

        //
        // GET: Scheduler/Home/Employees
        [HttpGet]
        public IActionResult PayRates()
        {
            return View();
        }
    }
}
