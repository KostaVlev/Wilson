using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Wilson.Accounting.Data.DataAccess;
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
    }
}
