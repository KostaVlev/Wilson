using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wilson.Accounting.Data.DataAccess;

namespace Wilson.Web.Areas.Accounting.Controllers
{
    [Area(Constants.Areas.Accounting)]
    [Authorize(Roles = Constants.Roles.Administrator + ", " + Constants.Roles.Accouter)]
    public abstract class AccountingBaseController : Controller
    {
        public AccountingBaseController(IAccountingWorkData accountingWorkData)
        {
            this.AccountingWorkData = accountingWorkData;
        }

        public IAccountingWorkData AccountingWorkData { get; set; }
    }
}
