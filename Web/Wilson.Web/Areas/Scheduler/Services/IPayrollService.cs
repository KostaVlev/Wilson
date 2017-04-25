using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wilson.Scheduler.Core.Entities;
using Wilson.Scheduler.Data.DataAccess;
using Wilson.Web.Areas.Scheduler.Models.PayrollViewModels;
using Wilson.Web.Areas.Scheduler.Models.SharedViewModels;

namespace Wilson.Web.Areas.Scheduler.Services
{
    public interface IPayrollService
    {
        /// <summary>
        /// Scheduler context that will be used for Db Operations.
        /// </summary>
        ISchedulerWorkData SchedulerWorkData { get; set; }

        /// <summary>
        /// Creates <see cref="IndexViewModel"/>
        /// </summary>
        /// <returns><see cref="IndexViewModel"/></returns>
        IndexViewModel PrepareIndexViewModel();

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
        /// <param name="periodFrom">Beginning of the search period in format Month/Year - xx/yyyy.</param>
        /// /// <param name="periodTo">End of the search period in format Month/Year - xx/yyyy.</param>
        /// <param name="employeeId">Employee who will be checked for paychecks for the period if
        /// NULL checks all the employees.</param>
        /// <returns>Collection of <see cref="EmployeeViewModel"/></returns>
        Task<IEnumerable<EmployeeViewModel>> FindEmployeesPayshecks(string periodFrom, string periodTo, string employeeId);

        /// <summary>
        /// Converts string date xx/yyyy to the <see cref="DateTime"/>, first or last day of the month.
        /// </summary>
        /// <param name="period">String to convert.</param>
        /// <param name="isBeggingOfThePeriod">Indicates if is the beginning or end of the period.</param>
        /// <returns>If true returns first day of the month otherwise last day if the month.</returns>
        /// <exception cref="ArgumentException">Thrown if the period cannot be converted.</exception>
        DateTime TryParsePeriod(string period, bool isBeggingOfThePeriod = true);
    }
}
