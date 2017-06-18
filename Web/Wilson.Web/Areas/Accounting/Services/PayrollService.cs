using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Accounting.Data.DataAccess;
using Wilson.Accounting.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Wilson.Web.Areas.Accounting.Services
{
    public class PayrollService : Service, IPayrollService
    {
        public PayrollService(IAccountingWorkData accountingWorkData, IMapper mapper)
            : base(accountingWorkData, mapper)
        {
        }

        public async Task<List<SelectListItem>> GetAccountingEmployeeOptions()
        {
            return await this.GetEmployeeOptions();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesWihtPayrolls(string employeeId = null)
        {
            if (string.IsNullOrEmpty(employeeId))
            {
                return await this.AccountingWorkData.Employees.GetAllAsync(i => i.Include(x => x.Paycheks));                
            }
            else
            {
                return await this.AccountingWorkData.Employees
                    .FindAsync(e => e.Id == employeeId, i => i.Include(x => x.Paycheks));
            }
        }

        public async Task<Paycheck> FindEmployeePaycheck(string employeeId, string paychekId)
        {
            var result = await this.AccountingWorkData.Employees
                .FindAsync(e => e.Id == employeeId && !e.IsFired, i => i.Include(x => x.Paycheks));

            return result.FirstOrDefault().Paycheks.FirstOrDefault(p => p.Id == paychekId);
        }

        public async Task AddPayment(Paycheck paychek, DateTime paymentDate, decimal amount)
        {
            paychek.AddPayment(paymentDate, amount);

            await this.AccountingWorkData.CompleteAsync();
        }
    }
}
