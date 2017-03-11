using Microsoft.AspNetCore.Mvc;
using Wilson.Accounting.Data.DataAccess;

namespace Wilson.Web.Areas.Accounting.Controllers
{
    public class HomeController : AccountingBaseController
    {
        public HomeController(IAccountingWorkData accountingWorkData)
            : base(accountingWorkData)
        {
        }

        //
        // GET: /Accounting/Home
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
