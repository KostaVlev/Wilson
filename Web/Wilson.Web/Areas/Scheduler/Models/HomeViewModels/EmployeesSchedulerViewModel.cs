using System.Collections.Generic;
using Wilson.Web.Areas.Scheduler.Models.SharedViewModels;

namespace Wilson.Web.Areas.Scheduler.Models.HomeViewModels
{
    public class EmployeesSchedulerViewModel
    {
        public IEnumerable<EmployeeViewModel> Employees { get; set; }

        public IEnumerable<ProjectViewModel> Projects { get; set; }
    }
}
