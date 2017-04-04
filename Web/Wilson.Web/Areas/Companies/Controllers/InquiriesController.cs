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
using Wilson.Web.Areas.Companies.Utilities;

namespace Wilson.Web.Areas.Companies.Controllers
{
    public class InquiriesController : CompanyBaseController
    {
        private readonly IAttachmnetProcessor attachmnetProcessor;

        public InquiriesController(ICompanyWorkData companyWorkData, IMapper mapper, IAttachmnetProcessor attachmnetProcessor)
            : base(companyWorkData, mapper)
        {
            this.attachmnetProcessor = attachmnetProcessor;
        }

        //
        // GET: Companies/Inquiries/Index
        [HttpGet]
        public async Task<IActionResult> Index(string message)
        {
            ViewData["StatusMessage"] = message ?? "";

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
                .Include(r => r.ReceivedBy)
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
                var inquiry = this.Mapper.Map<CreateViewModel, Inquiry>(model);

                // The current Inquiry is received by the current user.
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var currentUser = await this.CompanyWorkData.Users.FindAsync(x => x.Id == currentUserId);
                inquiry.ReceivedById = currentUser.FirstOrDefault().EmployeeId;

                // Many-To-Many relationship schema has to be updated.
                foreach (var assigneeId in model.AssigneesIds)
                {
                    this.CompanyWorkData.InquiryEmployee.Add(new InquiryEmployee() { EmployeeId = assigneeId, InquiryId = inquiry.Id });
                }

                // If there are attached files, update the Attachments schema.
                if (model.Attachments != null && model.Attachments.Count() > 0)
                {
                    try
                    {
                        var attachments = await this.attachmnetProcessor.PrepareForUpload(model.Attachments);
                        foreach (var attachment in attachments)
                        {
                            attachment.InquiryId = inquiry.Id;
                        }

                        this.CompanyWorkData.Attachments.AddRange(attachments);
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {

                        return RedirectToAction(nameof(Create), new { Message = ex.Message });
                    }
                    catch (ArgumentException ex)
                    {

                        return RedirectToAction(nameof(Create), new { Message = ex.Message });
                    }                   
                }
                
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
        /// <example>await CreateCtreateViewModel(...)</example>
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