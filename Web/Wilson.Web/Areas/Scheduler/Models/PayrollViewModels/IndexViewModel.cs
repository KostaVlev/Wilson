using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Wilson.Web.Areas.Scheduler.Models.PayrollViewModels
{
    public class IndexViewModel
    {
        [StringLength(7, ErrorMessage = Constants.ValidationMessages.Error, MinimumLength = 7)]
        [Display(Name = "Period")]
        public string Period { get; set; }

        public IEnumerable<SelectListItem> PeriodOptions { get; set; }
    }
}
