using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wilson.Scheduler.Core.Entities;
using Wilson.Scheduler.Core.Enumerations;
using Wilson.Scheduler.Data.DataAccess;
using Wilson.Web.Areas.Scheduler.Models.HomeViewModels;
using Wilson.Web.Areas.Scheduler.Models.SharedViewModels;

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
                var employees = await this.SchedulerWorkData.Employees.FindAsync(
                    e => model.Employees.Any(me => me.NewSchedule.EmployeeId == e.Id), 
                    x => x.Include(s => s.Schedules));

                foreach (var employee in employees)
                {
                    var scheduleModel = model.Employees.Where(e => e.NewSchedule.EmployeeId == employee.Id).FirstOrDefault().NewSchedule;

                    // Since this is the today schedule we assign today to the Date property.
                    scheduleModel.Date = DateTime.Now;

                    var schedule = this.Mapper.Map<ScheduleViewModel, Schedule>(scheduleModel);
                    employee.AddNewSchedule(schedule);
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
            if (date == null)
            {
                return NotFound();
            }

            string dateFormat = Constants.DateTimeFormats.Short;
            var shrotDate = date.ToString(dateFormat);
            var schedules = await this.SchedulerWorkData.Schedules.FindAsync(s => s.Date.ToString(dateFormat) == shrotDate, x => x
            .Include(e => e.Employee)
            .Include(p => p.Project));
            
            if (schedules == null || schedules.Count() == 0)
            {
                return NotFound();
            }

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
                    schedule.Update(model.WorkHours, model.ExtraWorkHours, model.ScheduleOption, model.ProjectId);
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

            var schedule = await this.FindSchedule(id);
            if (schedule == null)
            {
                return NotFound();
            }

            var scheduleModel = this.Mapper.Map<Schedule, ScheduleViewModel>(schedule);
            await this.SetupScheduleModelForEdit(scheduleModel);

            return View(scheduleModel);
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
                var schedule = await this.FindSchedule(model.Id);
                if (schedule == null)
                {
                    return NotFound();
                }

                schedule.Update(model.WorkHours, model.ExtraWorkHours, model.ScheduleOption, model.ProjectId);

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
        // GET: Scheduler/Home/Search
        [HttpGet]
        public async Task<IActionResult> Search()
        {            
            return View(await this.SetupSearchModel());
        }

        //
        // POST: Scheduler/Home/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(SearchViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                if (model.From > model.To)
                {
                    ModelState.AddModelError("", Constants.ValidationMessages.IncorectDate);
                    return View(await this.SetupSearchModel());
                }

                var employeesScgedulesModel = await this.GetSchedulesGroupedByEmployee(model.From, model.To, model.EmployeeId, model.ProjectId, model.ScheduleOption);
                if (employeesScgedulesModel != null)
                {
                    var returnModel = await this.SetupSearchModel();
                    returnModel.EmployeesShcedules = employeesScgedulesModel;

                    return View(returnModel);
                }
            }

            ModelState.AddModelError("", Constants.ValidationMessages.Error);
            return View(await this.SetupSearchModel());
        }

        //
        // GET: Scheduler/Home/PayRolls
        [HttpGet]
        public IActionResult PayRolls()
        {
            return View();
        }

        /// <summary>
        /// Asynchronous method that finds Schedule by given ID.
        /// </summary>
        /// <param name="id">ID to search.</param>
        /// <returns><see cref="Schedule"/></returns>
        /// <example>await FindSchedule(...)</example>
        private async Task<Schedule> FindSchedule(string id)
        {
            var employees = await this.SchedulerWorkData.Employees.GetAllAsync(e => e.Include(s => s.Schedules));
            var schedule = employees.FirstOrDefault(e => e.Schedules.Any(s => s.Id == id)).Schedules.FirstOrDefault(s => s.Id == id);

            return schedule;
        }

        /// <summary>
        /// Gets the schedule options form <see cref="ScheduleOption"/> which are used for drop-down lists.
        /// </summary>
        /// <returns><see cref="List{T}"/> where {T} is <see cref="SelectListItem"/> with value equal to the
        /// <see cref="ScheduleOption"/> value and text taken the <see cref="DisplayAttribute"/></returns>
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

        /// <summary>
        /// Gets the <see cref="ScheduleOption"/> name from the <see cref="DisplayAttribute"/>.
        /// </summary>
        /// <param name="scheduleOption"></param>
        /// <returns>The name as <see cref="string"/>.</returns>
        private string GetShceduleOptionName(ScheduleOption scheduleOption)
        {
            string name = scheduleOption
                .GetType()
                .GetMember(scheduleOption.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>().Name;

            return name;
        }

        /// <summary>
        /// Creates Collection of Project as options for drop-down lists.
        /// </summary>
        /// <param name="projectModels">Collection of <see cref="ProjectViewModel"/> that will be used.</param>
        /// <returns><see cref="List{T}"/> where {T} is <see cref="SelectListItem"/> with 
        /// value the <see cref="ProjectViewModel.Id"/> and text <see cref="ProjectViewModel.ShortName"/></returns>
        private List<SelectListItem> GetProjectOptions(IEnumerable<ProjectViewModel> projectModels)
        {
            return projectModels.Select(x => new SelectListItem() { Value = x.Id, Text = x.ShortName }).ToList();
        }

        /// <summary>
        /// Creates Collection of Employees as options for drop-down lists.
        /// </summary>
        /// <param name="employeeModels">Collection of <see cref="EmployeeConciseViewModel"/> that will be used.</param>
        /// <returns><see cref="List{T}"/> where {T} is <see cref="SelectListItem"/> with 
        /// value the <see cref="EmployeeConciseViewModel.Id"/> and text <see cref="EmployeeConciseViewModel.ToString()"/></returns>
        private List<SelectListItem> GetEmployeeOptions(IEnumerable<EmployeeConciseViewModel> employeeModels)
        {
            return employeeModels.Select(x => new SelectListItem() { Value = x.Id, Text = x.ToString() }).ToList();
        }

        /// <summary>
        /// Asynchronous method that populates <see cref="EmployeeViewModel.NewSchedule"/> property.
        /// </summary>
        /// <param name="employeeModels">Collection of <see cref="EmployeeViewModel"/> which will be processed.</param>
        /// <example>await SetupEmployeeNewSchedule(...)</example>
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

        /// <summary>
        /// Asynchronous method that prepares <see cref="ScheduleViewModel"/> for displaying.
        /// </summary>
        /// <param name="employeeModels">Collection of <see cref="ScheduleViewModel"/> which will be processed.</param>
        /// <example>await SetupScheduleModelForEdit(...)</example>
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

        /// <summary>
        /// Asynchronous method that prepares <see cref="ScheduleViewModel"/> for displaying.
        /// </summary>
        /// <param name="employeeModel"><see cref="ScheduleViewModel"/> model which will be processed.</param>
        /// <example>await SetupScheduleModelForEdit(...)</example>
        private async Task SetupScheduleModelForEdit(ScheduleViewModel scheduleModel)
        {
            var projects = await this.SchedulerWorkData.Projects.FindAsync(x => x.IsActive);
            var projectModels = this.Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projects);

            var projectOptions = this.GetProjectOptions(projectModels);
            var scheduleOptions = this.GetScheduleOptions();

            scheduleModel.ProjectOptions = projectOptions;
            scheduleModel.ScheduleOptions = scheduleOptions;
        }

        /// <summary>
        /// Asynchronous method that creates <see cref="EmployeesShceduleViewModel"/> ready for displaying.
        /// </summary>
        /// <example>await PrepareEmployeesShceduleViewModel()</example>
        private async Task<EmployeesShceduleViewModel> PrepareEmployeesShceduleViewModel()
        {
            // Group the schedules by employee.
            var groupedSchedulesByEmployee = await this.GetSchedulesGroupedByEmployee();

            // Get all the employees to schedule.
            var employees = await this.SchedulerWorkData.Employees.GetAllAsync();
            var employeeModels = this.Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees).ToList();

            // Check if today's schedule was created.
            var today = DateTime.Now.ToString(Constants.DateTimeFormats.Short);
            bool isTodayShceduleCreated = groupedSchedulesByEmployee.Values.Any(s => s.Any(x => x.Date.ToString(Constants.DateTimeFormats.Short) == today));

            if (!isTodayShceduleCreated)
            {
                await this.SetupEmployeeNewSchedule(employeeModels);
            }

            return new EmployeesShceduleViewModel()
            {
                EmployeesShcedules = groupedSchedulesByEmployee,
                Employees = employeeModels,
                IsTodayScheduleCreated = isTodayShceduleCreated,
                Today = today
            };
        }

        /// <summary>
        /// Asynchronous method that creates collection of <see cref="ScheduleViewModel"/> for each Employee in order to
        /// meet the requirements of given parameters.
        /// </summary>
        /// <param name="from">Start date. Default start date is 7 days ago. Null will select all dates.</param>
        /// <param name="to">End date. Default end date is Today. Null will select all dates.</param>
        /// <param name="employeeId">The Employee ID. Null will select all Employees.</param>
        /// <param name="projectId">The Project ID. Null will select all Projects.</param>
        /// <param name="scheduleOption"><see cref="ScheduleOption"/>. Null will select all Schedule Options.</param>
        /// <returns><see cref="IDictionary{TKey, TValue}"/> where {TKey} is <see cref="EmployeeConciseViewModel"/> and 
        /// {TValue} is <see cref="List{T}"/> where {T} is <see cref="ScheduleViewModel"/> ordered by Date.</returns>
        private async Task<IDictionary<EmployeeConciseViewModel, List<ScheduleViewModel>>> GetSchedulesGroupedByEmployee(
            DateTime? from = null, DateTime? to = null, string employeeId = null, string projectId = null, ScheduleOption? scheduleOption = null)
        {
            IEnumerable<Schedule> schedules;

            // Because the date will be xx.xx.xxxx 12:00 AM for a correct search we need to add 23 hours 59 min and 59 sec or just 1 day.
            if (to.HasValue)
            {
                to = to.Value.AddDays(1);
            }

            if (!from.HasValue && !to.HasValue && employeeId == null && projectId == null && !scheduleOption.HasValue)
            {
                // Get schedules for the last 7 days.
                var sevenDaysAgo = DateTime.Now.AddDays(-7);
                schedules = await this.SchedulerWorkData.Schedules.FindAsync(s => s.Date >= sevenDaysAgo, x => x
                .Include(e => e.Employee)
                .Include(p => p.Project));
            }
            else if (from.HasValue && to.HasValue && employeeId == null && projectId == null && !scheduleOption.HasValue)
            {
                schedules = await this.SchedulerWorkData.Schedules.FindAsync(
                    s => s.Date >= from && s.Date <= to, x => x
                   .Include(e => e.Employee)
                   .Include(p => p.Project));
            }
            else if (from.HasValue && to.HasValue && employeeId == null && projectId == null && scheduleOption.HasValue)
            {
                schedules = await this.SchedulerWorkData.Schedules.FindAsync(
                    s => s.Date >= from && s.Date <= to && s.ScheduleOption == scheduleOption, x => x
                   .Include(e => e.Employee)
                   .Include(p => p.Project));
            }
            else if (from.HasValue && to.HasValue && employeeId != null && projectId == null && !scheduleOption.HasValue)
            {
                schedules = await this.SchedulerWorkData.Schedules.FindAsync(
                    s => s.Date >= from && s.Date <= to && s.EmployeeId == employeeId, x => x
                   .Include(e => e.Employee)
                   .Include(p => p.Project));
            }
            else if (from.HasValue && to.HasValue && employeeId != null && projectId == null && scheduleOption.HasValue)
            {
                schedules = await this.SchedulerWorkData.Schedules.FindAsync(
                    s => s.Date >= from && s.Date <= to && s.EmployeeId == employeeId && s.ScheduleOption == scheduleOption, x => x
                   .Include(e => e.Employee)
                   .Include(p => p.Project));
            }
            else if (from.HasValue && to.HasValue && employeeId == null && projectId != null && !scheduleOption.HasValue)
            {
                schedules = await this.SchedulerWorkData.Schedules.FindAsync(
                    s => s.Date >= from && s.Date <= to && s.ProjectId == projectId, x => x
                   .Include(e => e.Employee)
                   .Include(p => p.Project));
            }
            else if (from.HasValue && to.HasValue && employeeId == null && projectId != null && scheduleOption.HasValue)
            {
                schedules = await this.SchedulerWorkData.Schedules.FindAsync(
                    s => s.Date >= from && s.Date <= to && s.ProjectId == projectId && s.ScheduleOption == scheduleOption, x => x
                   .Include(e => e.Employee)
                   .Include(p => p.Project));
            }
            else if (from.HasValue && to.HasValue && employeeId != null && projectId != null && !scheduleOption.HasValue)
            {
                schedules = await this.SchedulerWorkData.Schedules.FindAsync(
                    s => s.Date >= from && s.Date <= to && s.ProjectId == projectId && s.EmployeeId == employeeId, x => x
                   .Include(e => e.Employee)
                   .Include(p => p.Project));
            }
            else if (from.HasValue && to.HasValue && employeeId != null && projectId != null && scheduleOption.HasValue)
            {
                schedules = await this.SchedulerWorkData.Schedules.FindAsync(
                    s => s.Date >= from && s.Date <= to && s.ProjectId == projectId && s.EmployeeId == employeeId && s.ScheduleOption == scheduleOption, x => x
                   .Include(e => e.Employee)
                   .Include(p => p.Project));
            }
            else
            {
                throw new InvalidOperationException("Get Schedules Grouped By Employee cannot be completed. Check the method parameters.");
            }

            var scheduleModels = this.Mapper.Map<IEnumerable<Schedule>, IEnumerable<ScheduleViewModel>>(schedules);
            foreach (var schedule in scheduleModels)
            {
                schedule.ScheduleOptionName = this.GetShceduleOptionName(schedule.ScheduleOption);
            }

            // Group the schedules by employee.
            var groupedSchedulesByEmployee = scheduleModels
                .GroupBy(e => e.Employee.Id)
                .Select(grp => new { Key = grp.FirstOrDefault().Employee, Value = grp.OrderBy(x => x.Date).ToList() })
                .ToDictionary(x => x.Key, x => x.Value);

            return groupedSchedulesByEmployee;
        }

        /// <summary>
        /// Asynchronous method that creates <see cref="SearchViewModel"/>.
        /// </summary>
        /// <returns><see cref="SearchViewModel"/></returns>
        /// <example>await SetupSearchModel()</example>
        private async Task<SearchViewModel> SetupSearchModel()
        {
            var projects = await this.SchedulerWorkData.Projects.FindAsync(x => x.IsActive);
            var employees = await this.SchedulerWorkData.Employees.GetAllAsync();

            var projectModels = this.Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projects);
            var employeeModels = this.Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeConciseViewModel>>(employees);

            var to = DateTime.Now;
            var from = to.AddDays(-7);
            var projectOptions = this.GetProjectOptions(projectModels);            
            var scheduleOptions = this.GetScheduleOptions();
            var employeeOptions = this.GetEmployeeOptions(employeeModels);

            return new SearchViewModel()
                {
                    To = to,
                    From = from,
                    ProjectOptions = projectOptions,
                    ScheduleOptions = scheduleOptions,
                    EmployeeOptions = employeeOptions
                };
        }
    }
}
