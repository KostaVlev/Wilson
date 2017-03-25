using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wilson.Companies.Data.DataAccess;

namespace Wilson.Web.Controllers
{
    [Authorize]
    public class CompanyBaseController : Controller
    {        
        public CompanyBaseController(ICompanyWorkData companyWorkData)
        {
            this.CompanyWorkData = companyWorkData;
        }

        public ICompanyWorkData CompanyWorkData { get; set; }
    }
}