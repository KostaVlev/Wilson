using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Wilson.Companies.Data.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Wilson.Web.Areas.Companies.Models.HomeViewModels;

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

        //
        // GET: Companies/Company/Inquiries
        [HttpGet]
        public async Task<IActionResult> Inquiries()
        {
            var inquiries = await this.CompanyWorkData.Inquiries.GetAllAsync();
            var models = new List<InquiryViewModel>();
            foreach (var item in inquiries)
            {
                var model = new InquiryViewModel() { Id = item.Id };
                models.Add(model);
            }

            return View(models);
        }
    }
}
