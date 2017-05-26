using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Web.Areas.Scheduler.Services;

namespace Wilson.Web.Areas.Scheduler.Models.PayrollViewModels
{
    public class IndexViewModel
    {
        public string Message { get; set; }

        [StringLength(7, ErrorMessage = Constants.ValidationMessages.Error, MinimumLength = 7)]
        [Display(Name = "Period")]
        public string Period { get; set; }

        public IEnumerable<SelectListItem> PeriodOptions { get; set; }

        public static IndexViewModel Create(IPayrollService services, string message = null)
        {
            return new IndexViewModel()
            {
                Message = message ?? string.Empty,
                PeriodOptions = services.GetPeriodsOptions()
            };
        }        
    }
}
