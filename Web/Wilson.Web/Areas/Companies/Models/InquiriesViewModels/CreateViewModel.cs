using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Wilson.Companies.Core.Entities;
using Wilson.Companies.Data.DataAccess;

namespace Wilson.Web.Areas.Companies.Models.InquiriesViewModels
{
    public class CreateViewModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Received At")]
        public DateTime ReceivedAt { get; set; } = DateTime.Today;

        [Required]
        [StringLength(900, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 70)]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Choose Customer.")]
        [StringLength(36)]
        public string CustomerId { get; set; }

        [Required(ErrorMessage = "Assign at least one employee.")]
        public string[] AssigneesIds { get; set; }

        [Display(Name = "Customer")]
        public IEnumerable<CompanyViewModel> Customers { get; set; }

        [Display(Name = "Assign Employees")]
        public IEnumerable<EmployeeViewModel> Assignees { get; set; }

        [Display(Name = "Attach Files")]
        public IEnumerable<IFormFile> Attachments { get; set; }

        public async static Task<CreateViewModel> CreateAsync(ICompanyWorkData companyWorkData, IMapper mapper)
        {
            var settings = await companyWorkData.Settings.GetAllAsync(i => i
                .Include(x => x.HomeCompany)
                .ThenInclude(x => x.Employees));

            var company = settings.FirstOrDefault().HomeCompany;
            var customers = await companyWorkData.Companies.FindAsync(x => x.Id != company.Id);

            return new CreateViewModel()
            {
                Customers = mapper.Map<IEnumerable<Company>, IEnumerable<CompanyViewModel>>(customers),
                Assignees = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(company.Employees)
            };
        }

        public async static Task<CreateViewModel> ReBuildAsync(CreateViewModel model, ICompanyWorkData companyWorkData, IMapper mapper)
        {
            var settings = await companyWorkData.Settings.GetAllAsync(i => i
                .Include(x => x.HomeCompany)
                .ThenInclude(x => x.Employees));

            var company = settings.FirstOrDefault().HomeCompany;
            var customers = await companyWorkData.Companies.FindAsync(x => x.Id != company.Id);

            model.Customers = mapper.Map<IEnumerable<Company>, IEnumerable<CompanyViewModel>>(customers);
            model.Assignees = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(company.Employees);

            return model;
        }
    }
}
