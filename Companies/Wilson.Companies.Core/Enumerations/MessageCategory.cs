using System.ComponentModel.DataAnnotations;

namespace Wilson.Companies.Core.Enumerations
{
    public enum MessageCategory
    {
        [Display(Name = "Accounting")]
        Accounting = 1,

        [Display(Name = "Schedule")]
        Schedule = 2,

        [Display(Name = "Inventory")]
        Inventory = 3,

        [Display(Name = "Projects")]
        Projects = 4,

        [Display(Name = "Other")]
        Other = 5
    }
}
