using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Accounting.Core.Entities;

namespace Wilson.Web.Areas.Accounting.Services
{
    public interface IPayrollService
    {
        /// <summary>
        /// Asynchronous method that creates Collection of Employees as options for drop-down lists for Accounting module.
        /// </summary>
        /// <returns><see cref="List{T}"/> where {T} is <see cref="SelectListItem"/> with 
        /// value the employee Id and text the employee Name</returns>
        Task<List<SelectListItem>> GetAccountingEmployeeOptions();

        /// <summary>
        /// Asynchronous method that creates Collection of Employees loaded with their payrolls.
        /// </summary>
        /// <param name="employeeId">The employee id who has to be leaded. Null if we need all the employees</param>
        /// <returns><see cref="List{T}"/> where {T} is <see cref="Employee"/>
        Task<IEnumerable<Employee>> GetEmployeesWihtPayrolls(string employeeId = null);
         
        /// <summary>
        /// Asynchronous method finds paycheck by Id for given employee identified by its Id.
        /// </summary>
        /// <param name="employeeId">the Employee Id.</param>
        /// <param name="paychekId">The paycheck Id.</param>
        /// <returns><see cref="Paycheck"/></returns>
        Task<Paycheck> FindEmployeePaycheck(string employeeId, string paychekId);

        /// <summary>
        /// Asynchronous method finds paychecks by Id.
        /// </summary>
        /// <param name="paychekIds">Collection of paycheck Ids.</param>
        /// <returns>Collection of <see cref="Paycheck"/></returns>
        Task<IEnumerable<Paycheck>> FindEmployeePaychecks(IEnumerable<string> paychekIds);

        /// <summary>
        /// Asynchronous method that adds payment to given paycheck.
        /// </summary>
        /// <param name="paychek">The paycheck that has to be payed.</param>
        /// <param name="paymentDate">The payment date.</param>
        /// <param name="amount">The payed amount.</param>
        Task AddPayment(Paycheck paychek, DateTime paymentDate, decimal amount);

        /// <summary>
        /// Asynchronous method that adds payments to given collection of paychecks. 
        /// </summary>
        /// <param name="paycheks">Collection of <see cref="Paycheck"/> that will be processed.</param>
        /// <param name="paymentDate">The payment date. The date is the same for all the payments that will be made.</param>
        /// <param name="paycheckIdAmountPair">Collection of "paycheck id - amount that will be payed" pair.</param>
        Task AddPayments(IEnumerable<Paycheck> paycheks, DateTime paymentDate, IDictionary<string, decimal> paycheckIdAmountPair);
    }
}
