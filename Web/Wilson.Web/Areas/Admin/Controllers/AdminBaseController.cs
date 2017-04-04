using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wilson.Companies.Core.Entities;
using Wilson.Companies.Core.Interfaces;
using Wilson.Companies.Data.DataAccess;

namespace Wilson.Web.Areas.Admin.Controllers
{
    [Area(Constants.Areas.Admin)]
    [Authorize(Roles = Constants.Roles.Administrator)]    
    public class AdminBaseController : Controller
    {
        private ISettings settings;

        public AdminBaseController(ICompanyWorkData companyWorkData, IMapper mapper)
        {
            this.CompanyWorkData = companyWorkData;
            this.Mapper = mapper;
        }

        public ICompanyWorkData CompanyWorkData { get; set; }

        public IMapper Mapper { get; set; }

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