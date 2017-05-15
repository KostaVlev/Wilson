using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wilson.Accounting.Data.DataAccess;
using Wilson.Companies.Core.Entities;
using Wilson.Companies.Data.DataAccess;
using Wilson.Scheduler.Data.DataAccess;

namespace Wilson.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger logger;

        public HomeController(
            ICompanyWorkData companyWorkData, 
            ISchedulerWorkData schedulerWorkData,
            IAccountingWorkData accountingWorkData,
            IMapper mapper,
            ILoggerFactory loggerFactory)
            : base(companyWorkData, schedulerWorkData, accountingWorkData, mapper)
        {
            this.logger = loggerFactory.CreateLogger<HomeController>();
        }

        public IActionResult Index(string message)
        {
            ViewData["StatusMessage"] = message ?? "";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }        

        private async Task SaveChangesInAllDbContexts()
        {
            await this.SchedulerWorkData.CompleteAsync();
            await this.CompanyWorkData.CompleteAsync();
        }

        private async Task SyncCompanySchedulerEmployees()
        {
            var companyEmployees = await this.CompanyWorkData.Employees.GetAllAsync();
            var schedulerEmployees = await this.SchedulerWorkData.Employees.GetAllAsync();

            // Get the Base Pay Rate that will be assigned to each new employee.
            var basePayRate = await this.SchedulerWorkData.PayRates.SingleOrDefaultAsync(x => x.IsBaseRate);

            // Get the new employees.
            var employeesToAddToSheduler = companyEmployees.Where(x => !schedulerEmployees.Any() || !schedulerEmployees.Any(e => e.Id == x.Id));
            var newSchedulerEmployees =
                this.Mapper.Map<IEnumerable<Employee>, IEnumerable<Scheduler.Core.Entities.Employee>>(employeesToAddToSheduler);

            // Assigned Base Pay Rate for each new employee.
            foreach (var employee in newSchedulerEmployees)
            {
                employee.ApplayPayRate(basePayRate);
            }

            this.SchedulerWorkData.Employees.AddRange(newSchedulerEmployees);
        }
    }
}
