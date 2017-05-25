using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wilson.Scheduler.Core.Entities;
using Wilson.Scheduler.Core.Enumerations;
using Wilson.Web.Areas.Scheduler.Models.HomeViewModels;

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
            return await this.SchedulerWorkData.Employees
                .FindAsync(e => !e.IsFired, x => x.Include(s => s.Schedules).ThenInclude(p => p.Project));
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
                
        public IEnumerable<EmployeeViewModel> FindEmployeeSchedules(IEnumerable<Employee> employees,
            DateTime? from = null, DateTime? to = null, string employeeId = null, string projectId = null, ScheduleOption? scheduleOption = null)
        {
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
                throw new InvalidOperationException("Can not find Employee schedules");
            }

            return employeeModels;
        }

        public async Task<List<SelectListItem>> GetShdeduleProjectOptions()
        {
            return await this.GetProjectOptions();
        }

        public async Task<List<SelectListItem>> GetShdeduleEmployeeOptions()
        {
            return await this.GetEmployeeOptions();
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
