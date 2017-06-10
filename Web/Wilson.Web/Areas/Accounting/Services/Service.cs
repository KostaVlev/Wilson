using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Accounting.Data.DataAccess;
using Wilson.Accounting.Core.Entities;

namespace Wilson.Web.Areas.Accounting.Services
{
    public class Service : IService
    {
        public Service(IAccountingWorkData accountingWorkData, IMapper mapper)
        {
            this.AccountingWorkData = accountingWorkData;
            this.Mapper = mapper;            
        }

        public IAccountingWorkData AccountingWorkData { get; set; }

        public IMapper Mapper { get; set; }
                
        public async Task<IEnumerable<Employee>> Employees()
        {
            return await this.AccountingWorkData.Employees.FindAsync(e => !e.IsFired);
        }

        public async Task<List<SelectListItem>> GetEmployeeOptions()
        {
            var employees = await this.Employees();
            return employees.Select(x => new SelectListItem() { Value = x.Id, Text = x.ToString() }).ToList();
        }
    }
}
