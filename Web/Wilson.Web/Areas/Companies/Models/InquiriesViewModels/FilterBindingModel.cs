using System;
using System.ComponentModel.DataAnnotations;

namespace Wilson.Web.Areas.Companies.Models.InquiriesViewModels
{
    public class FilterBindingModel
    {
        [DataType(DataType.Date)]
        public DateTime From { get; set; } = DateTime.Now.AddMonths(-1);

        [DataType(DataType.Date)]
        public DateTime To { get; set; } = DateTime.Now;
        
        [StringLength(36)]
        public string CustomerId { get; set; }
        
        [StringLength(36)]
        public string AssigneeId { get; set; }
    }
}
