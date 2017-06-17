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
    }
}
