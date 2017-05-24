using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wilson.Companies.Core.Entities;
using Wilson.Companies.Data.DataAccess;
using Wilson.Web.Areas.Companies.Models.InquiriesViewModels;

namespace Wilson.Web.Areas.Companies.Controllers
{
    public class InquiriesController : CompanyBaseController
    {
        public InquiriesController(ICompanyWorkData companyWorkData, IMapper mapper)
            : base(companyWorkData, mapper)
        {
        }

        //
        // GET: Companies/Inquiries/Filter
        [HttpGet]
        public async Task<IActionResult> Filter()
        {
            return View(await FilterViewModel.Create(this.CompanyWorkData, this.Mapper));
        }

        //
        // POST: Companies/Inquiries/Filter
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Filter(FilterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(await FilterViewModel.ReBuild(model, this.CompanyWorkData, this.Mapper));
            }

            return View(await FilterViewModel.CreateWithFilter(model, this.CompanyWorkData, this.Mapper));
        }

        //
        // GET: Companies/Inquiries/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(await CreateViewModel.CreateAsync(this.CompanyWorkData, this.Mapper));
        }


        //
        // POST: Companies/Inquiries/Create
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(await CreateViewModel.ReBuildAsync(model, this.CompanyWorkData, this.Mapper));
            }

            // The current Inquiry is received by the current user.
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = await this.CompanyWorkData.Users.FindAsync(x => x.Id == currentUserId);
            if (currentUser.FirstOrDefault().Employee.Id == null)
            {
                ModelState.AddModelError(string.Empty, $"Only company employees can create Inquiries!");
                return View(await CreateViewModel.ReBuildAsync(model, this.CompanyWorkData, this.Mapper));
            }

            var inquiry = Inquiry.Create(model.Description, currentUser.FirstOrDefault().Employee.Id, model.CustomerId);
            inquiry.AddAssignees(model.AssigneesIds);
            inquiry.AddAttachments(model.Attachments);

            this.CompanyWorkData.Inquiries.Add(inquiry);
            await this.CompanyWorkData.CompleteAsync();

            return RedirectToAction(nameof(Filter));
        }
    }
}