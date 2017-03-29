using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wilson.Companies.Data.DataAccess;
using AutoMapper;

namespace Wilson.Web.Controllers
{
    public class HomeController : CompanyBaseController
    {
        public HomeController(ICompanyWorkData companyWorkData, IMapper mapper)
            : base(companyWorkData, mapper)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
