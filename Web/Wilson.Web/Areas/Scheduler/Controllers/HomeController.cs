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
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Web.Areas.Scheduler.Models.HomeViewModels;
using Wilson.Scheduler.Core.Enumerations;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

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
            // Get schedules for the last 7 days.
            var sevenDaysAgo = DateTime.Now.AddDays(-7);
            var schedules = await this.SchedulerWorkData.Schedules.FindAsync(s => s.Date >= sevenDaysAgo, x => x
            .Include(e => e.Employee)
            .Include(p => p.Project));

            schedules.OrderBy(s => s.Date);

            var scheduleModels = this.Mapper.Map<IEnumerable<Schedule>, IEnumerable<ScheduleViewModel>>(schedules);
            foreach (var schedule in scheduleModels)
            {
                schedule.ScheduleOptionName = this.GetShceduleOptionName(schedule.ScheduleOption);
            }

            // Get all the employees to schedule.
            var employees = await this.SchedulerWorkData.Employees.GetAllAsync();
            var employeeModels = this.Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees).ToList();

            await this.SetupEmployeeNewSchedule(employeeModels);

            // Group the schedules by employee.
            var groupedSchedulesByEmployee = scheduleModels
                .GroupBy(e => e.Employee.Id)
                .Select(grp => new { Key = grp.FirstOrDefault().Employee, Value = grp.ToList() })
                .ToDictionary(x => x.Key, x => x.Value);

            return View(new EmployeesShceduleViewModel() { EmployeesShcedules = groupedSchedulesByEmployee, Employees = employeeModels });
        }

        //
        // POST: Scheduler/Home/EmployeesScheduler
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeesScheduler(EmployeesShceduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employeeModels = model.Employees;
                foreach (var employee in employeeModels)
                {
                    var schedule = this.Mapper.Map<ScheduleViewModel, Schedule>(employee.NewSchedule);

                    // Since this is the today schedule we assign today to the Date property.
                    schedule.Date = DateTime.Now;
                    if (schedule.ScheduleOption != ScheduleOption.AtWork)
                    {
                        schedule.ProjectId = null;
                    }

                    this.SchedulerWorkData.Schedules.Add(schedule);
                }

                await this.SchedulerWorkData.CompleteAsync();

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(EmployeesScheduler));
        }

        //
        // GET: Scheduler/Home/Employees
        [HttpGet]
        public IActionResult PayRates()
        {
            return View();
        }

        private List<SelectListItem> GetScheduleOptions()
        {
            var scheduleOptions = Enum.GetValues(typeof(ScheduleOption)).Cast<ScheduleOption>().Select(x => new SelectListItem
            {
                // Try to get the Schedule Option name from the DisplayAttribute.
                Text = x.GetType()
                        .GetMember(x.ToString())
                        .FirstOrDefault()
                        .GetCustomAttribute<DisplayAttribute>().Name ?? x.ToString(),
                Value = ((int)x).ToString()
            }).ToList();

            return scheduleOptions;
        }

        private string GetShceduleOptionName(ScheduleOption scheduleOption)
        {
            string name = scheduleOption
                .GetType()
                .GetMember(scheduleOption.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>().Name;

            return name;
        }

        private List<SelectListItem> GetProjectOptions(IEnumerable<ProjectViewModel> projectModels)
        {
            return projectModels.Select(x => new SelectListItem() { Value = x.Id, Text = x.ShortName }).ToList();            
        }

        private async Task SetupEmployeeNewSchedule(IEnumerable<EmployeeViewModel> employeeModels)
        {
            // Make collection of ProjectViewModels that will be used for drop-down list. Select only active Projects.
            var projects = await this.SchedulerWorkData.Projects.FindAsync(x => x.IsActive);
            var projectModels = this.Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projects);

            var projectOptions = this.GetProjectOptions(projectModels);
            var scheduleOptions = this.GetScheduleOptions();

            foreach (var employee in employeeModels)
            {
                var lastSchedule = employee.Schedules.LastOrDefault();
                if (lastSchedule != null)
                {
                    employee.NewSchedule = lastSchedule;
                }
                else
                {
                    employee.NewSchedule.EmployeeId = employee.Id;
                }

                employee.NewSchedule.ProjectOptions = projectOptions;
                employee.NewSchedule.ScheduleOptions = scheduleOptions;
                employee.NewSchedule.ScheduleOptionName = scheduleOptions[(int)employee.NewSchedule.ScheduleOption].Text;
            }
        }
    }
}
