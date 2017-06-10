using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Accounting.Data.DataAccess;

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
    }
}
