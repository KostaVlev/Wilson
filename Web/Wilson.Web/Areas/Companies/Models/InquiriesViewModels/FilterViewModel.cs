using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Wilson.Companies.Core.Entities;
using Wilson.Companies.Data.DataAccess;

namespace Wilson.Web.Areas.Companies.Models.InquiriesViewModels
{
    public class FilterViewModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Form")]
        public DateTime From { get; set; } = DateTime.Now.AddMonths(-1).Date;

        [DataType(DataType.Date)]
        [Display(Name = "To")]
        public DateTime To { get; set; } = DateTime.Now.AddDays(1).Date;

        [StringLength(36)]
        public string CustomerId { get; set; }

        [StringLength(36)]
        public string AssigneeId { get; set; }

        public IEnumerable<CompanyViewModel> Customers { get; set; }

        public IEnumerable<EmployeeViewModel> Assignees { get; set; }

        public IEnumerable<InquiryViewModel> Inquiries { get; set; }

        public static async Task<FilterViewModel> Create(
            ICompanyWorkData comapnyWorkData,
            IMapper mapper)
        {
            var inquiries = await GetInquiriesAsync(comapnyWorkData, x => true);

            var customers = inquiries.Select(x => x.Customer).Distinct();
            var assignees = inquiries.Select(x => x.Assignees.Select(e => e.Employee)).SelectMany(a => a).Distinct();

            return new FilterViewModel()
            {
                Customers = mapper.Map<IEnumerable<Company>, IEnumerable<CompanyViewModel>>(customers),
                Inquiries = mapper.Map<IEnumerable<Inquiry>, IEnumerable<InquiryViewModel>>(inquiries),
                Assignees = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(assignees)
            };
        }

        public async static Task<FilterViewModel> CreateWithFilter(
            FilterViewModel model,
            ICompanyWorkData comapnyWorkData,
            IMapper mapper)
        {
            var inquiries = await GetInquiriesAsync(comapnyWorkData, x => model.From <= x.ReceivedAt && model.To >= x.ReceivedAt);

            var customers = inquiries.Select(x => x.Customer).Distinct();
            var assignees = inquiries.Select(x => x.Assignees.Select(e => e.Employee)).SelectMany(a => a).Distinct();

            if (!string.IsNullOrEmpty(model.CustomerId) && !string.IsNullOrWhiteSpace(model.CustomerId))
            {
                inquiries = inquiries.Where(x => model.CustomerId == x.CustomerId);
            }

            if (!string.IsNullOrEmpty(model.AssigneeId) && !string.IsNullOrWhiteSpace(model.AssigneeId))
            {
                inquiries = inquiries.Where(x => x.Assignees.Any(a => model.AssigneeId == a.EmployeeId));
            }

            return new FilterViewModel()
            {
                Customers = mapper.Map<IEnumerable<Company>, IEnumerable<CompanyViewModel>>(customers),
                Inquiries = mapper.Map<IEnumerable<Inquiry>, IEnumerable<InquiryViewModel>>(inquiries),
                Assignees = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(assignees)
            };
        }

        public static async Task<FilterViewModel> ReBuild(
            FilterViewModel model,
            ICompanyWorkData comapnyWorkData,
            IMapper mapper)
        {
            var inquiries = await GetInquiriesAsync(comapnyWorkData, x => true);

            var customers = inquiries.Select(x => x.Customer).Distinct();
            var assignees = inquiries.Select(x => x.Assignees.Select(e => e.Employee)).SelectMany(a => a).Distinct();

            model.Customers = mapper.Map<IEnumerable<Company>, IEnumerable<CompanyViewModel>>(customers);
            model.Inquiries = mapper.Map<IEnumerable<Inquiry>, IEnumerable<InquiryViewModel>>(inquiries);
            model.Assignees = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(assignees);

            return model;
        }

        private static async Task<IEnumerable<Inquiry>> GetInquiriesAsync(
            ICompanyWorkData comapnyWorkData, Expression<Func<Inquiry, bool>> predicate)
        {
            return await comapnyWorkData.Inquiries.FindAsync(predicate, i => i
                .Include(x => x.ReceivedBy)
                .Include(x => x.Assignees)
                .ThenInclude(e => e.Employee)
                .Include(x => x.Customer)
                .Include(x => x.InfoRequests)
                .Include(x => x.Attachments)
                .Include(x => x.Offers));
        }
    }
}
