using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        // GET: Companies/Inquiries/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var inquiries = await this.CompanyWorkData.Inquiries.GetAllAsync(i => i.Include(c => c.Customer));
            var customers = inquiries.Select(x => x.Customer);
            var inquiryEmployee = await this.CompanyWorkData.InquiryEmployee
                .FindAsync(x => inquiries.Any(i => i.Assignees.Any(a => a.EmployeeId == x.EmployeeId)), x => x.Include(e => e.Employee));
            var assignees = inquiryEmployee.Select(x => x.Employee);

            var customerModels = this.Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyViewModel>>(customers);
            var assigneeModels = this.Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(assignees);

            var model = new FilterViewModel()
            {
                Customers = customerModels,
                Assignees = assigneeModels
            };

            return View(model);
        }

        //
        // GET: Companies/Inquiries/ShowAll
        [HttpGet]
        public async Task<IActionResult> ShowAll()
        {
            var inquiries = await this.CompanyWorkData.Inquiries.GetAllAsync(i => i
                .Include(r => r.RecivedBy)
                .Include(c => c.Customer)
                .Include(ir => ir.InfoRequests)
                .Include(a => a.Attachmnets)
                .Include(o => o.Offers));

            var assignees = await this.CompanyWorkData.InquiryEmployee
                .FindAsync(x => inquiries.Any(i => i.Assignees.Any(a => a.EmployeeId == x.EmployeeId)), x => x.Include(e => e.Employee));

            foreach (var inquiry in inquiries)
            {
                inquiry.Assignees = assignees.Where(x => x.InquiryId == inquiry.Id).ToArray();
            }

            var models = this.Mapper.Map<IEnumerable<Inquiry>, IEnumerable<InquiryViewModel>>(inquiries);

            return View(models);
        }

        //
        // POST: Companies/Inquiries/FilterByDate
        [HttpPost]
        public async Task<IActionResult> Filter(FilterBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var inquiries = await this.CompanyWorkData.Inquiries.FindAsync(x => 
                (model.From <= x.ReceivedAt && model.To >= x.ReceivedAt),
                i => i
                .Include(x => x.RecivedBy)
                .Include(x => x.Assignees)
                .Include(x => x.Customer)
                .Include(x => x.InfoRequests)
                .Include(x => x.Attachmnets)
                .Include(x => x.Offers));

                if (!string.IsNullOrEmpty(model.CustomerId) && !string.IsNullOrWhiteSpace(model.CustomerId))
                {
                    inquiries = inquiries.Where(x => model.CustomerId == x.CustomerId);
                }

                if (!string.IsNullOrEmpty(model.AssigneeId) && !string.IsNullOrWhiteSpace(model.AssigneeId))
                {
                    inquiries = inquiries.Where(x => x.Assignees.Any(a => model.AssigneeId == a.EmployeeId));
                }

                var assignees = await this.CompanyWorkData.InquiryEmployee
                    .FindAsync(x => inquiries.Any(i => i.Assignees.Any(a => a.EmployeeId == x.EmployeeId)), x => x.Include(e => e.Employee));

                foreach (var inquiry in inquiries)
                {
                    inquiry.Assignees = assignees.Where(x => x.InquiryId == inquiry.Id).ToArray();
                }

                var models = this.Mapper.Map<IEnumerable<Inquiry>, IEnumerable<InquiryViewModel>>(inquiries);

                return View(models);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}