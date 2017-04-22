using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wilson.Scheduler.Core.Entities;
using Wilson.Web.Areas.Scheduler.Models.PayrollViewModels;
using Wilson.Web.Areas.Scheduler.Models.SharedViewModels;

namespace Wilson.Web.Areas.Scheduler.Services
{
    public class PayrollService : Service, IPayrollService
    {
        public PayrollService(IMapper mapper)
            : base(mapper)
        {
        }

        public async Task<ReviewPaychecksViewModel> PrepareReviewPaychecksViewModel()
        {
            var employeeOptions = await this.GetEmployeeOptions();
            var periodOptions = this.GetPeriodsOptions();

            return new ReviewPaychecksViewModel() { EmployeeOptions = employeeOptions, PeriodOptions = periodOptions };
        }

        public async Task<IEnumerable<Employee>> GetEmployeesWithoutPaycheks(DateTime? date)
        {
            var dateValue = date.GetValueOrDefault();
            if (dateValue == default(DateTime))
            {
                dateValue = DateTime.Now;
            }

            var employees = await this.SchedulerWorkData.Employees.GetAllAsync(x => x
                .Include(s => s.Schedules)
                .Include(p => p.Paychecks)
                .Include(pr => pr.PayRate));

            var employeesWithoutPaychecks = employees.Where(e => !e.Paychecks.Any(p =>
                p.Date.Month == dateValue.Month && p.Date.Year == dateValue.Year));

            return employeesWithoutPaychecks;
        }

        public async Task<IEnumerable<EmployeeViewModel>> FindEmployeesPayshecks(string period, string employeeId)
        {
            IEnumerable<Employee> query;
            if (string.IsNullOrEmpty(employeeId))
            {
                query = await this.SchedulerWorkData.Employees.GetAllAsync(x => x.Include(p => p.Paychecks));
            }
            else
            {
                query = await this.SchedulerWorkData.Employees.FindAsync(e => e.Id == employeeId, x => x.Include(p => p.Paychecks));
            }

            var date = this.TryParsePeriod(period);
            var employees = query.Where(e => e.Paychecks.Any(
                p => p.From.ToString(Constants.DateTimeFormats.Short) == date.ToString(Constants.DateTimeFormats.Short))).ToList();

            var employeeModels = this.Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            return employeeModels;
        }

        private DateTime TryParsePeriod(string period)
        {
            try
            {
                var month = int.Parse(period.Substring(0, 2));
                var year = int.Parse(period.Substring(3, 4));

                return new DateTime(year, month, 1);
            }
            catch
            {

                throw new ArgumentException("Period cannot be parsed.", "period");
            }
        }
    }
}
