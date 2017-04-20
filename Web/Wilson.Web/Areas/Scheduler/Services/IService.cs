using AutoMapper;
using Wilson.Scheduler.Data.DataAccess;

namespace Wilson.Web.Areas.Scheduler.Services
{
    public interface IService
    {
        IMapper Mapper { get; set; }
    }
}
