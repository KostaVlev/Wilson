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

        public async Task<IEnumerable<EmployeeViewModel>> FindEmployeesPayshecks(string periodFrom, string periodTo, string employeeId)
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

            var dateFrom = this.TryParsePeriod(periodFrom);
            var dateTo = this.TryParsePeriod(periodTo, false);
            var employees = query.Where(e => e.Paychecks.Any(p => p.From >= dateFrom && p.To <= dateTo));

            var employeeModels = this.Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            foreach (var employee in employeeModels)
            {
                employee.Paychecks = employee.Paychecks.Where(p => p.From >= dateFrom && p.To <= dateTo).OrderBy(f => f.From);
            }

            return employeeModels;
        }

        public DateTime TryParsePeriod(string period, bool isBeggingOfThePeriod = true)
        {
            try
            {
                var month = int.Parse(period.Substring(0, 2));
                var year = int.Parse(period.Substring(3, 4));
                var firstDayOfTheMonth = new DateTime(year, month, 1);
                var lastDayOfTheMonth = firstDayOfTheMonth.AddMonths(1).AddTicks(-1);
                if (isBeggingOfThePeriod)
                {
                    return firstDayOfTheMonth;
                }
                else
                {
                    return lastDayOfTheMonth;
                }
            }
            catch
            {

                throw new ArgumentException("Period cannot be parsed.", "period");
            }
        }
    }
}
