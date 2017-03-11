using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wilson.Accounting.Data.DataAccess;

namespace Wilson.Web.Areas.Accounting.Controllers
{
    [Authorize]
    [Area("Accounting")]
    public abstract class AccountingBaseController : Controller
    {
        public AccountingBaseController(IAccountingWorkData accountingWorkData)
        {
            this.AccountingWorkData = accountingWorkData;
        }

        public IAccountingWorkData AccountingWorkData { get; set; }
    }
}
