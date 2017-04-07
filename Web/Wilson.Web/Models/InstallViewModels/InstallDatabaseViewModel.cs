using System.ComponentModel.DataAnnotations;
using Wilson.Web.Models.SharedViewModels;

namespace Wilson.Web.Models.InstallViewModels
{
    public class InstallDatabaseViewModel
    {
        public UserViewModel User { get; set; }
        
        [Display(Name = "Would you like to seed data into the database?")]
        public bool SeedData { get; set; }
        
        public CompanyViewModel Company { get; set; }
    }
}
