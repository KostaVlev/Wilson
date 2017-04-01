using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wilson.Companies.Data.DataAccess;

namespace Wilson.Web.Areas.Companies.Controllers
{
    public class HomeController : CompanyBaseController
    {
        public HomeController(ICompanyWorkData companyWorkData, IMapper mapper)
            : base(companyWorkData, mapper)
        {
        }

        //
        // GET: Companies/Company/Index
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }        
    }
}
