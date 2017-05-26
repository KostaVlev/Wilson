using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Scheduler.Core.Entities;
using Wilson.Web.Areas.Scheduler.Models.PayrollViewModels;

namespace Wilson.Web.Areas.Scheduler.Services
{
    public class PayrollService : Service, IPayrollService
    {
        public PayrollService(IMapper mapper)
            : base(mapper)
        {
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await this.Employees();
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

            var employees = await this.Employees();
            var employeesWithoutPaychecks = employees.Where(e => !e.Paychecks.Any(p =>
                p.Date.Month == dateValue.Month && p.Date.Year == dateValue.Year));

            return employeesWithoutPaychecks;
        }

        public async Task<IEnumerable<Employee>> FindEmployeesPayshecks(DateTime from, DateTime to, string employeeId)
        {
            var employees = await this.Employees();
            if (!string.IsNullOrEmpty(employeeId))
            {
                employees = employees.Where(e => e.Id == employeeId);
            }

            employees = employees.Where(e => e.Paychecks.Any(p => p.From.Date >= from.Date && p.To.Date <= to.Date));
            employees.ToList().ForEach(e => e.Paychecks.OrderBy(p => p.From));
            
            return employees;
        }

        public async Task<List<SelectListItem>> GetShdeduleEmployeeOptions()
        {
            return await this.GetEmployeeOptions();
        }

        public bool TryParsePeriod(string period, out DateTime date, bool isBeggingOfThePeriod = true)
        {
            try
            {
                var month = int.Parse(period.Substring(0, 2));
                var year = int.Parse(period.Substring(3, 4));
                var firstDayOfTheMonth = new DateTime(year, month, 1);
                if (isBeggingOfThePeriod)
                {
                    date = firstDayOfTheMonth;
                }
                else
                {
                    date = firstDayOfTheMonth.AddMonths(1).AddTicks(-1);
                }

                return true;
            }
            catch
            {
                date = default(DateTime);
                return false;
            }
        }

        public List<SelectListItem> GetPeriodsOptions()
        {
            var options = new List<SelectListItem>();
            var years = Enumerable.Range(DateTime.Today.AddYears(-5).Year, 6).Reverse();
            var months = Enumerable.Range(1, 12);

            foreach (var year in years)
            {
                foreach (var month in months)
                {
                    string optionText = CrteatePeriodOptionText(month, year);
                    var option = new SelectListItem()
                    {
                        Text = optionText,
                        Value = optionText
                    };

                    options.Add(option);
                }
            }

            return options;
        }

        private string CrteatePeriodOptionText(int month, int year)
        {
            string monthText = month.ToString().Length == 1 ? month.ToString().PadLeft(2, '0') : month.ToString();
            string yearText = year.ToString();

            return string.Format("{0}/{1}", monthText, yearText);
        }
    }
}
