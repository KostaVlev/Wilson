using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Wilson.Web.Areas.Scheduler.Services
{
    public interface IService
    {
        IMapper Mapper { get; set; }
                
        Task<List<SelectListItem>> GetProjectOptions();
                
        Task<List<SelectListItem>> GetEmployeeOptions();
    }
}
