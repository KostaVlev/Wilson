using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Wilson.Accounting.Data.DataAccess;
using Wilson.Web.Areas.Accounting.Models.HomeViewModels;
using Wilson.Web.Events.Interfaces;

namespace Wilson.Web.Areas.Accounting.Controllers
{
    public class HomeController : AccountingBaseController
    {
        public HomeController(IAccountingWorkData accountingWorkData, IMapper mapper, IEventsFactory eventsFactory)
            : base(accountingWorkData, mapper, eventsFactory)
        {
        }

        //
        // GET: /Accounting/Home
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //
        // GET: /Accounting/Payrolls
        [HttpGet]
        public async Task<IActionResult> Payroll()
        {
            return View(new PayrollViewModel());
        }
    }
}
