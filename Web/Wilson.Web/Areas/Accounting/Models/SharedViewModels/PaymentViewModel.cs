using System;
using System.ComponentModel.DataAnnotations;

namespace Wilson.Web.Areas.Accounting.Models.SharedViewModels
{
    public class PaymentViewModel
    {
        public DateTime Date { get; set; }
        
        [Required(ErrorMessage = "Amount is required.")]
        [Range(typeof(decimal), "0", "999999999", ErrorMessage = "Amount can't be less then 0.")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        public static PaymentViewModel Create()
        {
            return new PaymentViewModel() { Date = DateTime.Today, Amount = 0 };
        }
    }
}
