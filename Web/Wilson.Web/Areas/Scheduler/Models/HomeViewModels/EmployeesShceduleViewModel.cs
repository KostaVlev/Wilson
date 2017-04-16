using System.Collections.Generic;
using Wilson.Web.Areas.Scheduler.Models.SharedViewModels;

namespace Wilson.Web.Areas.Scheduler.Models.HomeViewModels
{
    public class EmployeesShceduleViewModel
    {
        public IDictionary<EmployeeConciseViewModel, List<ScheduleViewModel>> EmployeesShcedules { get; set; }

        public IList<EmployeeViewModel> Employees { get; set; }
    }
}
