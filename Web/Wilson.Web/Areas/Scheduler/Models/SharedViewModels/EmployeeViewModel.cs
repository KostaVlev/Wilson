using System.Collections.Generic;

namespace Wilson.Web.Areas.Scheduler.Models.SharedViewModels
{
    public class EmployeeViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public IEnumerable<ScheduleViewModel> Schedules { get; set; }
    }
}
