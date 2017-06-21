using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Accounting.Core.Entities;
using Wilson.Web.Areas.Accounting.Models.SharedViewModels;
using Wilson.Web.Areas.Accounting.Services;

namespace Wilson.Web.Areas.Accounting.Models.HomeViewModels
{
    public class PayrollViewModel
    {
        [StringLength(36)]
        [Display(Name = "Employee")]
        public string EmployeeId { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Select From date.")]
        [Display(Name = "From")]        
        public DateTime From { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Select To date.")]
        [Display(Name = "To")]
        public DateTime To { get; set; }

        public decimal TotalPaycheksSum { get; set; }

        public decimal TotalPaymentsSum { get; set; }

        public decimal TotalDiffrenceToBePayed { get; set; }

        public IEnumerable<SelectListItem> EmployeeOptions { get; set; }

        public IEnumerable<EmployeeViewModel> Employees { get; set; }

        public static async Task<PayrollViewModel> CreateAsync(IPayrollService services)
        {
            return new PayrollViewModel()
            {
                From = DateTime.Today.AddMonths(-1),
                To = DateTime.Today,
                EmployeeOptions = await services.GetAccountingEmployeeOptions()
            };
        }

        public static async Task<PayrollViewModel> CreateAsync(
            DateTime from, 
            DateTime to, 
            string employeeId, 
            IPayrollService services,
            IMapper mapper)
        {
            var model = await PayrollViewModel.CreateAsync(services);
            var employees = await services.GetEmployeesWihtPayrolls(employeeId);
            var employeessWithFilteredPaycheks = services.FilterEmployeesPaycheksByDate(from, to, employees);

            model.Employees = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employeessWithFilteredPaycheks);
            model.From = from;
            model.To = to;
            model.TotalPaycheksSum = employees.Sum(e => e.Paycheks.Select(p => p.Total).Sum());
            model.TotalPaymentsSum = employees.Sum(e => e.Paycheks.Select(p => p.GetPaidAmount()).Sum());
            model.TotalDiffrenceToBePayed = model.TotalPaycheksSum - model.TotalPaymentsSum;

            return model;
        }

        public static async Task<PayrollViewModel> ReBuildAsync(PayrollViewModel model, IPayrollService services)
        {
            model.From = DateTime.Today.AddMonths(-1);
            model.To = DateTime.Today;
            model.EmployeeOptions = await services.GetAccountingEmployeeOptions();

            return model;
        }
    }
}
