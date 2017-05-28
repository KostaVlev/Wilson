using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wilson.Accounting.Data.DataAccess;
using Wilson.Web.Events.Interfaces;

namespace Wilson.Web.Areas.Accounting.Controllers
{
    [Area(Constants.Areas.Accounting)]
    [Authorize(Roles = Constants.Roles.Administrator + ", " + Constants.Roles.Accouter)]
    public abstract class AccountingBaseController : Controller
    {
        public AccountingBaseController(IAccountingWorkData accountingWorkData, IMapper mapper, IEventsFactory eventsFactory)
        {
            this.AccountingWorkData = accountingWorkData;
            this.Mapper = mapper;
            this.EventsFactory = eventsFactory;
        }

        public IAccountingWorkData AccountingWorkData { get; set; }

        public IMapper Mapper { get; set; }

        public IEventsFactory EventsFactory { get; set; }
    }
}
