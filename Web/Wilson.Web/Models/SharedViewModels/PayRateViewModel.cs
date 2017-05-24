using System.ComponentModel.DataAnnotations;

namespace Wilson.Web.Models.SharedViewModels
{
    public class PayRateViewModel
    {
        public bool IsBaseRate { get; set; }

        [Display(Name = "Hour Rate")]
        public decimal Hour { get; set; }

        [Display(Name = "Extra Hour Rate")]
        public decimal ExtraHour { get; set; }

        [Display(Name = "Holiday Hour Rate")]
        public decimal HoidayHour { get; set; }

        [Display(Name = "Business Trip Rate")]
        public decimal BusinessTripHour { get; set; }

        public static PayRateViewModel Create()
        {
            return new PayRateViewModel();
        }

        public static PayRateViewModel ReBuild(PayRateViewModel model)
        {
            return model;
        }
    }
}
