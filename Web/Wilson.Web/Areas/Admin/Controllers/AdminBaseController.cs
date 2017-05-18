using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wilson.Companies.Core.Interfaces;
using Wilson.Companies.Data.DataAccess;
using Wilson.Web.Events.Interfaces;

namespace Wilson.Web.Areas.Admin.Controllers
{
    [Area(Constants.Areas.Admin)]
    [Authorize(Roles = Constants.Roles.Administrator)]    
    public class AdminBaseController : Controller
    {
        private ISettings settings;

        public AdminBaseController(ICompanyWorkData companyWorkData, IMapper mapper, IEventsFactory eventsFactory)
        {
            this.CompanyWorkData = companyWorkData;
            this.Mapper = mapper;
            this.EventsFactory = eventsFactory;
        }

        public ICompanyWorkData CompanyWorkData { get; set; }

        public IMapper Mapper { get; set; }

        public IEventsFactory EventsFactory { get; set; }

        public void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public async Task<ISettings> GetSettings()
        {
            if (this.settings == null)
            {
                var result = await this.CompanyWorkData.Settings.GetAllAsync();
                this.settings = result.FirstOrDefault();
            }

            return this.settings;
        }
    }
}