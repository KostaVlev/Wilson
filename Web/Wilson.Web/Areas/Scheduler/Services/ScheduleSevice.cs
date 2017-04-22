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

namespace Wilson.Web.Areas.Scheduler.Services
{
    public class ScheduleSevice : Service, IScheduleSevice
    {
        public ScheduleSevice(IMapper mapper)
            : base(mapper)
        {
        }        

        public async Task<Schedule> FindSchedule(string id)
        {
            var employees = await this.SchedulerWorkData.Employees.GetAllAsync(e => e.Include(s => s.Schedules));
            var schedule = employees.FirstOrDefault(e => e.Schedules.Any(s => s.Id == id)).Schedules.FirstOrDefault(s => s.Id == id);

            return schedule;
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

        public string GetShceduleOptionName(ScheduleOption scheduleOption)
        {
            string name = scheduleOption
                .GetType()
                .GetMember(scheduleOption.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>().Name;

            return name;
        }        

        public async Task SetupEmployeeNewSchedule(IEnumerable<EmployeeViewModel> employeeModels)
        {
            // Make collection of ProjectViewModels that will be used for drop-down list. Select only active Projects.
            var projects = await this.SchedulerWorkData.Projects.FindAsync(x => x.IsActive);
            var projectModels = this.Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projects);

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
                employee.NewSchedule.ScheduleOptionName = scheduleOptions[(int)employee.NewSchedule.ScheduleOption].Text;
            }
        }

        public async Task SetupScheduleModelForEdit(IEnumerable<ScheduleViewModel> scheduleModels)
        {
            var projects = await this.SchedulerWorkData.Projects.FindAsync(x => x.IsActive);
            var projectModels = this.Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projects);

            var projectOptions = await this.GetProjectOptions();
            var scheduleOptions = this.GetScheduleOptions();

            foreach (var scheduleModel in scheduleModels)
            {
                scheduleModel.ProjectOptions = projectOptions;
                scheduleModel.ScheduleOptions = scheduleOptions;
            }
        }

        public async Task SetupScheduleModelForEdit(ScheduleViewModel scheduleModel)
        {
            var projects = await this.SchedulerWorkData.Projects.FindAsync(x => x.IsActive);
            var projectModels = this.Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projects);

            var projectOptions = await this.GetProjectOptions();
            var scheduleOptions = this.GetScheduleOptions();

            scheduleModel.ProjectOptions = projectOptions;
            scheduleModel.ScheduleOptions = scheduleOptions;
        }

        public async Task<EmployeesShceduleViewModel> PrepareEmployeesShceduleViewModel()
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

        public async Task<IDictionary<EmployeeConciseViewModel, List<ScheduleViewModel>>> GetSchedulesGroupedByEmployee(
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

        public async Task<SearchViewModel> SetupSearchModel()
        {
            var projects = await this.SchedulerWorkData.Projects.FindAsync(x => x.IsActive);
            var employees = await this.SchedulerWorkData.Employees.GetAllAsync();

            var projectModels = this.Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projects);
            var employeeModels = this.Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeConciseViewModel>>(employees);

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
                EmployeeOptions = employeeOptions
            };
        }
    }
}
