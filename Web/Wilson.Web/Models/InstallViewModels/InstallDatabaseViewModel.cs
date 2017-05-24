using System.ComponentModel.DataAnnotations;
using Wilson.Web.Models.SharedViewModels;

namespace Wilson.Web.Models.InstallViewModels
{
    public class InstallDatabaseViewModel
    {
        public AdminUserViewModel User { get; set; }
        
        [Display(Name = "Would you like to seed data into the database?")]
        public bool SeedData { get; set; }
        
        public CompanyViewModel Company { get; set; }

        public PayRateViewModel PayRate { get; set; }

        public static InstallDatabaseViewModel Create()
        {
            return new InstallDatabaseViewModel()
            {
                User = AdminUserViewModel.Create(),
                Company = CompanyViewModel.Create(),
                PayRate = PayRateViewModel.Create()
            };
        }

        public static InstallDatabaseViewModel ReBuild(InstallDatabaseViewModel model)
        {
            model.User = AdminUserViewModel.ReBuild(model.User);
            model.Company = CompanyViewModel.ReBuild(model.Company);
            model.PayRate = PayRateViewModel.ReBuild(model.PayRate);

            return model;
        }
    }
}
