using System.Collections.Generic;
using Wilson.Web.Areas.Scheduler.Models.SharedViewModels;

namespace Wilson.Web.Areas.Scheduler.Models.HomeViewModels
{
    public class EmployeesShceduleViewModel
    {
        public string Today { get; set; }

        public bool IsTodayScheduleCreated { get; set; }

        public IDictionary<EmployeeConciseViewModel, List<ScheduleViewModel>> EmployeesShcedules { get; set; }

        public IList<EmployeeViewModel> Employees { get; set; }
    }
}
