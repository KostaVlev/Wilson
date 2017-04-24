using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wilson.Scheduler.Core.Entities;
using Wilson.Scheduler.Core.Enumerations;
using Wilson.Web.Areas.Scheduler.Models.HomeViewModels;
using Wilson.Web.Areas.Scheduler.Models.SharedViewModels;
using System.Linq.Expressions;

namespace Wilson.Web.Areas.Scheduler.Services
{
    public class ScheduleSevice : Service, IScheduleSevice
    {
        public ScheduleSevice(IMapper mapper)
            : base(mapper)
        {
        }

        public async Task<IEnumerable<Employee>> Employees()
        {
            var employees = await this.SchedulerWorkData.Employees
                .FindAsync(e => !e.IsFired, x => x.Include(s => s.Schedules).ThenInclude(p => p.Project));

            //var employeeModels = this.Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            return employees;
        }

        public async Task<Schedule> FindScheduleById(string id)
        {
            var employees = await this.Employees();
            var schedule = employees.FirstOrDefault(e => e.Schedules.Any(s => s.Id == id)).Schedules.FirstOrDefault(s => s.Id == id);

            return schedule;
        }

        public async Task<IEnumerable<Schedule>> FindAllSchedulesForDate(string dateTime)
        {
            var employees = await this.Employees();
            DateTime date = Convert.ToDateTime(dateTime);
            string dateFormat = Constants.DateTimeFormats.Short;
            var shrotDate = date.ToString(dateFormat);            
            var schedules = employees
                .Where(e => e.Schedules.Any(s => s.Date.ToString(dateFormat) == shrotDate))
                .SelectMany(e => e.Schedules)
                .Where(s => s.Date.ToString(dateFormat) == shrotDate);

            return schedules;
        }

        public async Task<EmployeesShceduleViewModel> PrepareEmployeesShceduleViewModel()
        {
            // Get all the employees to schedule.
            var employees = await this.Employees();
            var employeeModels = this.Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            // Check if today's schedule was created.
            var today = DateTime.Now.ToString(Constants.DateTimeFormats.Short);
            var employeesRecentSchedules = await this.FindEmployeeSchedules();
            bool isTodayShceduleCreated =
                employeeModels.Any(s => s.Schedules.Any(x => x.Date.ToString(Constants.DateTimeFormats.Short) == today));

            if (!isTodayShceduleCreated)
            {
                await this.SetupEmployeeNewSchedule(employeeModels);
            }

            return new EmployeesShceduleViewModel()
            {
                LastScheduledEmployees = employeesRecentSchedules,
                Employees = employeeModels,
                IsTodayScheduleCreated = isTodayShceduleCreated,
                Today = today
            };
        }

        public async Task SetupEmployeeNewSchedule(IEnumerable<EmployeeViewModel> employeeModels)
        {
            var projectOptions = await this.GetProjectOptions();
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
            }
        }

        public async Task<IEnumerable<ScheduleViewModel>> SetupScheduleModelForEdit(IEnumerable<Schedule> schedules)
        {
            var scheduleModels = this.Mapper.Map<IEnumerable<Schedule>, IEnumerable<ScheduleViewModel>>(schedules);
            var projectOptions = await this.GetProjectOptions();
            var scheduleOptions = this.GetScheduleOptions();

            foreach (var scheduleModel in scheduleModels)
            {
                scheduleModel.ProjectOptions = projectOptions;
                scheduleModel.ScheduleOptions = scheduleOptions;
            }

            return scheduleModels;
        }

        public async Task SetupScheduleModelForEdit(IEnumerable<ScheduleViewModel> scheduleModels)
        {
            var projectOptions = await this.GetProjectOptions();
            var scheduleOptions = this.GetScheduleOptions();

            foreach (var scheduleModel in scheduleModels)
            {
                scheduleModel.ProjectOptions = projectOptions;
                scheduleModel.ScheduleOptions = scheduleOptions;
            }
        }

        public async Task<ScheduleViewModel> SetupScheduleModelForEdit(Schedule schedule)
        {
            var scheduleModel = this.Mapper.Map<Schedule, ScheduleViewModel>(schedule);
            var projectOptions = await this.GetProjectOptions();
            var scheduleOptions = this.GetScheduleOptions();

            scheduleModel.ProjectOptions = projectOptions;
            scheduleModel.ScheduleOptions = scheduleOptions;

            return scheduleModel;
        }

        public async Task SetupScheduleModelForEdit(ScheduleViewModel scheduleModel)
        {
            var projectOptions = await this.GetProjectOptions();
            var scheduleOptions = this.GetScheduleOptions();

            scheduleModel.ProjectOptions = projectOptions;
            scheduleModel.ScheduleOptions = scheduleOptions;
        }

        public async Task<IEnumerable<EmployeeViewModel>> FindEmployeeSchedules(
            DateTime? from = null, DateTime? to = null, string employeeId = null, string projectId = null, ScheduleOption? scheduleOption = null)
        {
            var employees = await this.Employees();
            var employeeModels = this.Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            // Because the date will be xx.xx.xxxx 12:00 AM for a correct search we need to add 23 hours 59 min and 59 sec or just 1 day.
            if (to.HasValue)
            {
                to = to.Value.AddDays(1);
            }

            if (!from.HasValue && !to.HasValue && employeeId == null && projectId == null && !scheduleOption.HasValue)
            {
                // Get schedules for the last 3 days.
                var sevenDaysAgo = DateTime.Now.AddDays(-3);
                this.FilterEmployeeSchedules(employeeModels, s => s.Date >= sevenDaysAgo);
            }
            else if (from.HasValue && to.HasValue && employeeId == null && projectId == null && !scheduleOption.HasValue)
            {
                this.FilterEmployeeSchedules(employeeModels, s => s.Date >= from && s.Date <= to);
            }
            else if (from.HasValue && to.HasValue && employeeId == null && projectId == null && scheduleOption.HasValue)
            {
                this.FilterEmployeeSchedules(employeeModels, s => s.Date >= from && s.Date <= to && s.ScheduleOption == scheduleOption);
            }
            else if (from.HasValue && to.HasValue && employeeId != null && projectId == null && !scheduleOption.HasValue)
            {
                this.FilterEmployeeSchedules(employeeModels, s => s.Date >= from && s.Date <= to && s.EmployeeId == employeeId);
            }
            else if (from.HasValue && to.HasValue && employeeId != null && projectId == null && scheduleOption.HasValue)
            {
                this.FilterEmployeeSchedules(employeeModels,
                    s => s.Date >= from && s.Date <= to && s.EmployeeId == employeeId && s.ScheduleOption == scheduleOption);
            }
            else if (from.HasValue && to.HasValue && employeeId == null && projectId != null && !scheduleOption.HasValue)
            {
                this.FilterEmployeeSchedules(employeeModels, s => s.Date >= from && s.Date <= to && s.ProjectId == projectId);
            }
            else if (from.HasValue && to.HasValue && employeeId == null && projectId != null && scheduleOption.HasValue)
            {
                this.FilterEmployeeSchedules(employeeModels,
                    s => s.Date >= from && s.Date <= to && s.ProjectId == projectId && s.ScheduleOption == scheduleOption);
            }
            else if (from.HasValue && to.HasValue && employeeId != null && projectId != null && !scheduleOption.HasValue)
            {
                this.FilterEmployeeSchedules(employeeModels,
                    s => s.Date >= from && s.Date <= to && s.ProjectId == projectId && s.EmployeeId == employeeId);
            }
            else if (from.HasValue && to.HasValue && employeeId != null && projectId != null && scheduleOption.HasValue)
            {
                this.FilterEmployeeSchedules(employeeModels,
                    s => s.Date >= from &&
                    s.Date <= to &&
                    s.ProjectId == projectId &&
                    s.EmployeeId == employeeId &&
                    s.ScheduleOption == scheduleOption);
            }
            else
            {
                throw new InvalidOperationException("Get Schedules Grouped By Employee cannot be completed. Check the method parameters.");
            }

            return employeeModels;
        }

        public async Task<SearchViewModel> SetupSearchModel(IEnumerable<EmployeeViewModel> employees = null)
        {
            // Giving some start values for the SearchModel dates.
            var to = DateTime.Now;
            var from = to.AddDays(-7);

            var projectOptions = await this.GetProjectOptions();
            var scheduleOptions = this.GetScheduleOptions();
            var employeeOptions = await this.GetEmployeeOptions();

            return new SearchViewModel()
            {
                To = to,
                From = from,
                ProjectOptions = projectOptions,
                ScheduleOptions = scheduleOptions,
                EmployeeOptions = employeeOptions,
                Employees = employees != null ? employees.Where(e => e.Schedules != null && e.Schedules.Count() > 0) : employees
            };
        }

        public string GetShceduleOptionName(ScheduleOption scheduleOption)
        {
            string name = scheduleOption
                .GetType()
                .GetMember(scheduleOption.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>().Name;

            return name;
        }

        public List<SelectListItem> GetScheduleOptions()
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

        private void FilterEmployeeSchedules(IEnumerable<EmployeeViewModel> employees, Expression<Func<ScheduleViewModel, bool>> predicate)
        {
            foreach (var employee in employees)
            {
                employee.Schedules = employee.Schedules.AsQueryable().Where(predicate);
            }
        }       
    }
}
