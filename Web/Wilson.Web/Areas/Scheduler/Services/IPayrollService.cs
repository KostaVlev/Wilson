using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Scheduler.Core.Entities;
using Wilson.Scheduler.Data.DataAccess;
using Wilson.Web.Areas.Scheduler.Models.PayrollViewModels;

namespace Wilson.Web.Areas.Scheduler.Services
{
    public interface IPayrollService
    {
        /// <summary>
        /// Scheduler context that will be used for Db Operations.
        /// </summary>
        ISchedulerWorkData SchedulerWorkData { get; set; }
        
        /// <summary>
        /// Creates <see cref="ReviewPaychecksViewModel"/>.
        /// </summary>
        /// <returns><see cref="ReviewPaychecksViewModel"/></returns>
        Task<ReviewPaychecksViewModel> PrepareReviewPaychecksViewModel();

        /// <summary>
        /// Gets all the Employees without paychecks for the month of given date. If date is not specified then searches 
        /// the current month.
        /// </summary>
        /// <param name="date">The specified date.</param>
        /// <returns>Collection of <see cref="Employee"/></returns>
        Task<IEnumerable<Employee>> GetEmployeesWithoutPaycheks(DateTime? date);

        /// <summary>
        /// Finds all the employees who have paychecks for given period;
        /// </summary>
        /// <param name="from">Beginning of the search period in format Month/Year - xx/yyyy.</param>
        /// /// <param name="to">End of the search period in format Month/Year - xx/yyyy.</param>
        /// <param name="employeeId">Employee who will be checked for paychecks for the period if
        /// NULL checks all the employees.</param>
        /// <returns>Collection of <see cref="Employee"/></returns>
        Task<IEnumerable<Employee>> FindEmployeesPayshecks(DateTime from, DateTime to, string employeeId);

        /// <summary>
        /// Converts string date xx/yyyy to the <see cref="DateTime"/>, first or last day of the month.
        /// </summary>
        /// <param name="period">String to convert.</param>
        /// <param name="isBeggingOfThePeriod">Indicates if is the beginning or end of the period.</param>
        /// <returns>True if conversion is successful, otherwise false.</returns>
        bool TryParsePeriod(string period, out DateTime date, bool isBeggingOfThePeriod = true);
        
        /// <summary>
        /// Creates Collection of periods in format MM/YYYY.
        /// </summary>
        /// <returns><see cref="List{T}"/> where {T} is <see cref="SelectListItem"/> with 
        /// value the MM/YYYY and text MM/YYYY</returns>
        List<SelectListItem> GetPeriodsOptions();

        Task<List<SelectListItem>> GetShdeduleEmployeeOptions();

        Task<IEnumerable<Employee>> GetEmployees();
    }
}
