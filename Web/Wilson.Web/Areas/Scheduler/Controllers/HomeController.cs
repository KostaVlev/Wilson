using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wilson.Scheduler.Core.Entities;
using Wilson.Scheduler.Data.DataAccess;
using Wilson.Web.Areas.Scheduler.Models.HomeViewModels;
using Wilson.Web.Areas.Scheduler.Services;
using Wilson.Web.Events.Interfaces;

namespace Wilson.Web.Areas.Scheduler.Controllers
{
    public class HomeController : SchedulerBaseController
    {
        public HomeController(
            ISchedulerWorkData schedulerWorkData,
            IMapper mapper,
            IScheduleSevice scheduleSevice,
            IEventsFactory eventsFactory)
            : base(schedulerWorkData, mapper, eventsFactory)
        {
            this.ScheduleSevice = scheduleSevice;
            this.ScheduleSevice.SchedulerWorkData = schedulerWorkData;
        }

        public IScheduleSevice ScheduleSevice { get; set; }

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
            return View(await EmployeesShcedulerViewModel.CreateAsync(this.ScheduleSevice, this.Mapper));
        }

        //
        // POST: Scheduler/Home/EmployeesScheduler
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeesScheduler(EmployeesShcedulerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(await EmployeesShcedulerViewModel.ReBuildAsync(model, this.ScheduleSevice, this.Mapper));
            }

            var shcedules = new List<Schedule>();
            model.Employees.ToList().ForEach(e => shcedules.Add(Schedule.Create(
                DateTime.Now,
                e.NewSchedule.ScheduleOption,
                e.NewSchedule.EmployeeId,
                e.NewSchedule.ProjectId,
                e.NewSchedule.WorkHours,
                e.NewSchedule.ExtraWorkHours)));

            this.SchedulerWorkData.Schedules.AddRange(shcedules);
            await this.SchedulerWorkData.CompleteAsync();

            return View(await EmployeesShcedulerViewModel.CreateAsync(this.ScheduleSevice, this.Mapper));
        }

        //
        // GET: Scheduler/Home/EditSchedules
        [HttpGet]
        public async Task<IActionResult> EditSchedules(string dateTime)
        {
            if (!DateTime.TryParse(dateTime, out DateTime date))
            {
                return BadRequest($"No valid date. Try again and if the problem persist contact administrator.");
            }

            var schedules = await this.SchedulerWorkData.Schedules.FindAsync(
                s => s.Date.ToString(Constants.DateTimeFormats.Short) == date.ToString(Constants.DateTimeFormats.Short),
                i => i.Include(x => x.Employee));

            return View(await ScheduleViewModel.CreateCollectionForEditAsync(schedules, this.ScheduleSevice, this.Mapper));
        }

        //
        // POST: Scheduler/Home/EditSchedules
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSchedules(IEnumerable<ScheduleViewModel> models)
        {
            if (!ModelState.IsValid)
            {
                return View(await ScheduleViewModel.ReBuildCollectionForEditAsync(models, this.ScheduleSevice));
            }

            var schedules = await this.SchedulerWorkData.Schedules.FindAsync(
                x => models.Any(s => s.Id == x.Id), i => i.Include(x => x.Employee));

            foreach (var schedule in schedules)
            {
                var model = models.FirstOrDefault(x => x.Id == schedule.Id);
                schedule.Update(model.WorkHours, model.ExtraWorkHours, model.ScheduleOption, model.ProjectId);
            }

            await this.SchedulerWorkData.CompleteAsync();

            return RedirectToAction(nameof(EmployeesScheduler));
        }

        //
        // GET: Scheduler/Home/EditSchedules
        [HttpGet]
        public async Task<IActionResult> EditSchedule(string id)
        {
            var schedule = await this.SchedulerWorkData.Schedules.SingleOrDefaultAsync(s => s.Id == id, i => i.Include(x => x.Employee));
            if (schedule == null)
            {
                return NotFound($"Schedule not found. Please try again and if the problem persist contact the administrator");
            }

            return View(await ScheduleViewModel.CreateForEditAsync(schedule, this.ScheduleSevice, this.Mapper));
        }

        //
        // POST: Scheduler/Home/EditSchedules
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSchedule(ScheduleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(await ScheduleViewModel.ReBuildForEditAsync(model, this.ScheduleSevice));
            }

            var schedules = await this.SchedulerWorkData.Schedules.FindAsync(s => s.Id == model.Id, i => i.Include(x => x.Employee));
            var schedule = schedules.FirstOrDefault();
            if (schedule == null)
            {
                ModelState.AddModelError(
                    string.Empty,
                    $"Schedule to edit not found. Please try again and if the problem persist contact the administrator.");

                return View(await ScheduleViewModel.ReBuildForEditAsync(model, this.ScheduleSevice));
            }

            schedule.Update(model.WorkHours, model.ExtraWorkHours, model.ScheduleOption, model.ProjectId);
            await this.SchedulerWorkData.CompleteAsync();

            return RedirectToAction(nameof(EmployeesScheduler));
        }

        //
        // GET: Scheduler/Home/Search
        [HttpGet]
        public async Task<IActionResult> Search()
        {
            return View(await SearchViewModel.Create(this.ScheduleSevice, this.Mapper));
        }

        //
        // POST: Scheduler/Home/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(await SearchViewModel.ReBuild(model, this.ScheduleSevice, this.Mapper));
            }

            if (model.From > model.To)
            {
                ModelState.AddModelError(string.Empty, Constants.ValidationMessages.IncorectDate);
                return View(await SearchViewModel.ReBuild(model, this.ScheduleSevice, this.Mapper));
            }

            var employees = await this.ScheduleSevice.GetEmployees();
            var employeesResult = this.ScheduleSevice.FindEmployeeSchedules(
                employees, model.From, model.To, model.EmployeeId, model.ProjectId, model.ScheduleOption);

            return View(await SearchViewModel.Create(this.ScheduleSevice, this.Mapper, employeesResult));
        }
    }
}
