using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Wilson.Scheduler.Core.Entities;
using Wilson.Web.Areas.Scheduler.Services;
using Wilson.Web.Areas.Scheduler.Models.SharedViewModels;

namespace Wilson.Web.Areas.Scheduler.Models.HomeViewModels
{
    public class EmployeesShcedulerViewModel
    {
        public string Today { get; set; }

        public bool IsTodayScheduleCreated { get; set; }

        public IEnumerable<EmployeeViewModel> LastScheduledEmployees { get; set; }

        public IEnumerable<EmployeeViewModel> Employees { get; set; }

        public async static Task<EmployeesShcedulerViewModel> CreateAsync(
            IScheduleSevice services,
            IMapper mapper)
        {
            var employeesQuery = await services.SchedulerWorkData.Employees.FindAsync(x => !x.IsFired, i => i.Include(s => s.Schedules));
            var lastScheduledEmployees = services.FindEmployeeSchedules(employeesQuery);
            var employees = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employeesQuery);
            await services.SetupEmployeeNewSchedule(employees);
            bool isTodayShceduleCreated = employees.Any(e => e.Schedules.Any(s => s.Date.Date == DateTime.Today));


            return new EmployeesShcedulerViewModel()
            {
                LastScheduledEmployees = lastScheduledEmployees,
                Employees = employees,
                IsTodayScheduleCreated = isTodayShceduleCreated,
                Today = DateTime.Now.ToString(Constants.DateTimeFormats.Short)
            };
        }

        public async static Task<EmployeesShcedulerViewModel> ReBuildAsync(
            EmployeesShcedulerViewModel model,
            IScheduleSevice services,
            IMapper mapper)
        {
            var newModel = await EmployeesShcedulerViewModel.CreateAsync(services, mapper);

            model.Employees = newModel.Employees;
            model.LastScheduledEmployees = newModel.LastScheduledEmployees;
            model.IsTodayScheduleCreated = newModel.IsTodayScheduleCreated;
            model.Today = newModel.Today;

            return model;
        }
    }
}
