using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
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
        public async Task<IActionResult> Index(string message)
        {
            ViewData["StatusMessage"] = message ?? "";

            var inquiries = await this.CompanyWorkData.Inquiries.GetAllAsync(i => i
                .Include(c => c.Customer)
                .Include(a => a.Assignees)
                .ThenInclude(e => e.Employee));
            
            var customers = inquiries.Select(x => x.Customer).Distinct();
            var assignees = inquiries.Select(x => x.Assignees.Select(e => e.Employee)).SelectMany(a => a).Distinct();
            
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
                .Include(r => r.ReceivedBy)
                .Include(c => c.Customer)
                .Include(ir => ir.InfoRequests)
                .Include(a => a.Attachments)
                .Include(o => o.Offers)
                .Include(a => a.Assignees)
                .ThenInclude(e => e.Employee));

            var models = this.Mapper.Map<IEnumerable<Inquiry>, IEnumerable<InquiryViewModel>>(inquiries);

            return View(models);
        }

        //
        // POST: Companies/Inquiries/Filter
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Filter(FilterBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var inquiries = await this.CompanyWorkData.Inquiries.FindAsync(x =>
                (model.From <= x.ReceivedAt && model.To >= x.ReceivedAt),
                i => i
                .Include(x => x.ReceivedBy)
                .Include(x => x.Assignees)
                .ThenInclude(e => e.Employee)
                .Include(x => x.Customer)
                .Include(x => x.InfoRequests)
                .Include(x => x.Attachments)
                .Include(x => x.Offers));

                if (!string.IsNullOrEmpty(model.CustomerId) && !string.IsNullOrWhiteSpace(model.CustomerId))
                {
                    inquiries = inquiries.Where(x => model.CustomerId == x.CustomerId);
                }

                if (!string.IsNullOrEmpty(model.AssigneeId) && !string.IsNullOrWhiteSpace(model.AssigneeId))
                {
                    inquiries = inquiries.Where(x => x.Assignees.Any(a => model.AssigneeId == a.EmployeeId));
                }

                var models = this.Mapper.Map<IEnumerable<Inquiry>, IEnumerable<InquiryViewModel>>(inquiries);

                return View(models);
            }

            return RedirectToAction(nameof(Index));
        }

        //
        // GET: Companies/Inquiries/Create
        [HttpGet]
        public async Task<IActionResult> Create(string message)
        {
            ViewData["StatusMessage"] = message ?? "";

            if (User.IsInRole(Constants.Roles.Administrator))
            {
                return RedirectToAction(nameof(Index), new { Message = Constants.InquiriesMessages.OnlyForEmployees });
            }

            return View(await this.CreateCtreateViewModel(new CreateViewModel()));
        }


        //
        // POST: Companies/Inquiries/Create
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                // The current Inquiry is received by the current user.
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var currentUser = await this.CompanyWorkData.Users.FindAsync(x => x.Id == currentUserId);
                
                var inquiry = Inquiry.Create(model.Description, currentUser.FirstOrDefault().Employee.Id, model.CustomerId);
                inquiry.AddAssignees(model.AssigneesIds);
                inquiry.AddAttachments(model.Attachments);

                this.CompanyWorkData.Inquiries.Add(inquiry);                
                await this.CompanyWorkData.CompleteAsync();

                return RedirectToAction(nameof(Index));
            }
            
            return View(await this.CreateCtreateViewModel(model));
        }

        /// <summary>
        /// Async method which populates the properties <see cref="CreateViewModel.Customers"/> and <see cref="CreateViewModel.Assignees"/>
        /// with data for use of the Create View.
        /// </summary>
        /// <param name="model">The <see cref="CreateViewModel"/> which will be processed.</param>
        private async Task<CreateViewModel> CreateCtreateViewModel(CreateViewModel model)
        {
            var companies = await this.CompanyWorkData.Companies.GetAllAsync();
            var employees = await this.CompanyWorkData.Employees.GetAllAsync();            

            var companyModels = this.Mapper.Map<IEnumerable<Company>, IEnumerable<CompanyViewModel>>(companies);
            var employeeModels = this.Mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            model.Customers = companyModels;
            model.Assignees = employeeModels;

            return model;
        }
    }
}