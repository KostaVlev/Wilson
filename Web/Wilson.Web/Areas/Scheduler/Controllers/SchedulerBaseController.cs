using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wilson.Scheduler.Data.DataAccess;

namespace Wilson.Web.Areas.Scheduler.Controllers
{
    [Area(Constants.Areas.Scheduler)]
    [Authorize]
    public class SchedulerBaseController : Controller
    {
        public SchedulerBaseController(ISchedulerWorkData schedulerWorkData, IMapper mapper)
        {
            this.SchedulerWorkData = schedulerWorkData;
            this.Mapper = mapper;
        }

        public ISchedulerWorkData SchedulerWorkData { get; set; }

        public IMapper Mapper { get; set; }
    }
}
