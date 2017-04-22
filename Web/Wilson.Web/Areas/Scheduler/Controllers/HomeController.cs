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
using Wilson.Web.Areas.Scheduler.Models.SharedViewModels;
using Wilson.Web.Areas.Scheduler.Services;

namespace Wilson.Web.Areas.Scheduler.Controllers
{
    public class HomeController : SchedulerBaseController
    {
        public HomeController(ISchedulerWorkData schedulerWorkData, IMapper mapper, IScheduleSevice scheduleSevice)
            : base(schedulerWorkData, mapper)
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
        public async Task<IActionResult> EmployeesScheduler(string message)
        {
            ViewData["StatusMessage"] = message ?? "";
            return View(await this.ScheduleSevice.PrepareEmployeesShceduleViewModel());
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

            model = await this.ScheduleSevice.PrepareEmployeesShceduleViewModel();
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
            await this.ScheduleSevice.SetupScheduleModelForEdit(scheduleModels);

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
            await this.ScheduleSevice.SetupScheduleModelForEdit(models);

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

            var schedule = await this.ScheduleSevice.FindSchedule(id);
            if (schedule == null)
            {
                return NotFound();
            }

            var scheduleModel = this.Mapper.Map<Schedule, ScheduleViewModel>(schedule);
            await this.ScheduleSevice.SetupScheduleModelForEdit(scheduleModel);

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
                var schedule = await this.ScheduleSevice.FindSchedule(model.Id);
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
            await this.ScheduleSevice.SetupScheduleModelForEdit(model);
            return View(model);
        }

        //
        // GET: Scheduler/Home/Search
        [HttpGet]
        public async Task<IActionResult> Search()
        {            
            return View(await this.ScheduleSevice.SetupSearchModel());
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
                    return View(await this.ScheduleSevice.SetupSearchModel());
                }

                var employeesScgedulesModel = await this.ScheduleSevice.GetSchedulesGroupedByEmployee(
                    model.From, model.To, model.EmployeeId, model.ProjectId, model.ScheduleOption);

                if (employeesScgedulesModel != null)
                {
                    var returnModel = await this.ScheduleSevice.SetupSearchModel();
                    returnModel.EmployeesShcedules = employeesScgedulesModel;

                    return View(returnModel);
                }
            }

            ModelState.AddModelError("", Constants.ValidationMessages.Error);
            return View(await this.ScheduleSevice.SetupSearchModel());
        }               
    }
}
