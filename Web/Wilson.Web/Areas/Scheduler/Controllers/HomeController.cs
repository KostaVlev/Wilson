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
        public async Task<IActionResult> EmployeesScheduler(string message)
        {
            ViewData["StatusMessage"] = message ?? "";
            return View(await this.PrepareEmployeesShceduleViewModel());
        }

        //
        // POST: Scheduler/Home/EmployeesScheduler
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeesScheduler(EmployeesShceduleViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var employeeModels = model.Employees;
                foreach (var employee in employeeModels)
                {
                    var schedule = this.Mapper.Map<ScheduleViewModel, Schedule>(employee.NewSchedule);
                    schedule.Id = Guid.NewGuid().ToString();

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

            model = await this.PrepareEmployeesShceduleViewModel();
            return View(model);
        }

        //
        // GET: Scheduler/Home/EditSchedules
        [HttpGet]
        public async Task<IActionResult> EditSchedules(string dateTime)
        {
            if (dateTime == null)
            {
                return BadRequest();
            }

            DateTime date = Convert.ToDateTime(dateTime);
            string dateFormat = Constants.DateTimeFormats.Short;
            var shrotDate = date.ToString(dateFormat);
            var schedules = await this.SchedulerWorkData.Schedules.FindAsync(s => s.Date.ToString(dateFormat) == shrotDate, x => x
            .Include(e => e.Employee)
            .Include(p => p.Project));

            var scheduleModels = this.Mapper.Map<IEnumerable<Schedule>, IEnumerable<ScheduleViewModel>>(schedules);
            await this.SetupScheduleModelForEdit(scheduleModels);

            return View(scheduleModels);
        }

        //
        // POST: Scheduler/Home/EditSchedules
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSchedules(IEnumerable<ScheduleViewModel> models)
        {
            if (models == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var schedules = await this.SchedulerWorkData.Schedules.FindAsync(x => models.Any(s => s.Id == x.Id));
                foreach (var schedule in schedules)
                {
                    var model = models.FirstOrDefault(x => x.Id == schedule.Id);
                    this.UpdateSchedule(model, schedule);
                }

                var updateSuccess = await this.SchedulerWorkData.CompleteAsync();

                if (updateSuccess != 0)
                {
                    return RedirectToAction(nameof(EmployeesScheduler), new { Message = Constants.SuccessMessages.DatabaseUpdateSuccess });
                }
            }

            ModelState.AddModelError("", Constants.ExceptionMessages.DatabaseUpdateError);
            await this.SetupScheduleModelForEdit(models);
            return View(models);
        }

        //
        // GET: Scheduler/Home/EditSchedules
        [HttpGet]
        public async Task<IActionResult> EditSchedule(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var schedules = await this.SchedulerWorkData.Schedules.FindAsync(s => s.Id == id, x => x
            .Include(e => e.Employee)
            .Include(p => p.Project));

            if (schedules == null || schedules.Count() == 0)
            {
                return NotFound();
            }

            var scheduleModels = this.Mapper.Map<IEnumerable<Schedule>, IEnumerable<ScheduleViewModel>>(schedules);
            await this.SetupScheduleModelForEdit(scheduleModels);

            return View(scheduleModels.FirstOrDefault());
        }

        //
        // POST: Scheduler/Home/EditSchedules
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSchedule(ScheduleViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var schedules = await this.SchedulerWorkData.Schedules.FindAsync(s => s.Id == model.Id);
                var schedule = schedules.FirstOrDefault();
                this.UpdateSchedule(model, schedule);

                var updateSuccess = await this.SchedulerWorkData.CompleteAsync();

                if (updateSuccess != 0)
                {
                    return RedirectToAction(nameof(EmployeesScheduler), new { Message = Constants.SuccessMessages.DatabaseUpdateSuccess });
                }
            }

            ModelState.AddModelError("", Constants.ExceptionMessages.DatabaseUpdateError);
            await this.SetupScheduleModelForEdit(model);
            return View(model);
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

        private async Task SetupScheduleModelForEdit(IEnumerable<ScheduleViewModel> scheduleModels)
        {
            var projects = await this.SchedulerWorkData.Projects.FindAsync(x => x.IsActive);
            var projectModels = this.Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projects);

            var projectOptions = this.GetProjectOptions(projectModels);
            var scheduleOptions = this.GetScheduleOptions();

            foreach (var scheduleModel in scheduleModels)
            {
                scheduleModel.ProjectOptions = projectOptions;
                scheduleModel.ScheduleOptions = scheduleOptions;
            }
        }

        private async Task SetupScheduleModelForEdit(ScheduleViewModel scheduleModel)
        {
            var projects = await this.SchedulerWorkData.Projects.FindAsync(x => x.IsActive);
            var projectModels = this.Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projects);

            var projectOptions = this.GetProjectOptions(projectModels);
            var scheduleOptions = this.GetScheduleOptions();

            scheduleModel.ProjectOptions = projectOptions;
            scheduleModel.ScheduleOptions = scheduleOptions;
        }

        private async Task<EmployeesShceduleViewModel> PrepareEmployeesShceduleViewModel()
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

            // Check if today's schedule was created.
            var today = DateTime.Now.ToString(Constants.DateTimeFormats.Short);
            var todaySchedule = schedules.Where(x => x.Date.ToString(Constants.DateTimeFormats.Short) == today).FirstOrDefault();
            bool isTodayShceduleCreated = todaySchedule != null;

            if (!isTodayShceduleCreated)
            {
                await this.SetupEmployeeNewSchedule(employeeModels);
            }

            // Group the schedules by employee.
            var groupedSchedulesByEmployee = scheduleModels
                .GroupBy(e => e.Employee.Id)
                .Select(grp => new { Key = grp.FirstOrDefault().Employee, Value = grp.ToList() })
                .ToDictionary(x => x.Key, x => x.Value);

            return new EmployeesShceduleViewModel()
            {
                EmployeesShcedules = groupedSchedulesByEmployee,
                Employees = employeeModels,
                IsTodayScheduleCreated = isTodayShceduleCreated,
                Today = today
            };
        }

        private void UpdateSchedule(ScheduleViewModel source, Schedule target)
        {
            target.ProjectId = source.ProjectId;
            target.ScheduleOption = source.ScheduleOption;
            target.WorkHours = source.WorkHours;
            target.ExtraWorkHours = source.ExtraWorkHours;
        }
    }
}
