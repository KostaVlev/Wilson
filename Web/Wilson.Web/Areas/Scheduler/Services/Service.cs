using AutoMapper;

namespace Wilson.Web.Areas.Scheduler.Services
{
    public abstract class Service : IService
    {
        public Service(IMapper mapper)
        {
            this.Mapper = mapper;
        }       

        public IMapper Mapper { get; set; }
    }
}
