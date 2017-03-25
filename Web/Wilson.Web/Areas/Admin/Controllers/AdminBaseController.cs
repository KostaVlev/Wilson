using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wilson.Companies.Data.DataAccess;

namespace Wilson.Web.Areas.Admin.Controllers
{
    [Area(Constants.Areas.Admin)]
    [Authorize(Roles = Constants.Roles.Administrator)]    
    public class AdminBaseController : Controller
    {
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
    }
}