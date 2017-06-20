using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Accounting.Core.Entities;

namespace Wilson.Web.Areas.Accounting.Services
{
    public interface IPayrollService
    {
        Task<List<SelectListItem>> GetAccountingEmployeeOptions();

        Task<IEnumerable<Employee>> GetEmployeesWihtPayrolls(string employeeId = null);

        Task AddPayment(Paycheck paychek, DateTime paymentDate, decimal amount);

        Task<Paycheck> FindEmployeePaycheck(string employeeId, string paychekId);

        Task<IEnumerable<Paycheck>> FindEmployeePaychecks(IEnumerable<string> paychekIds);

        Task AddPayments(IEnumerable<Paycheck> paycheks, DateTime paymentDate, IDictionary<string, decimal> paycheckIdAmountPair);
    }
}
