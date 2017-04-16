using System.Collections.Generic;

namespace Wilson.Web.Areas.Scheduler.Models.SharedViewModels
{
    public class EmployeeViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ScheduleViewModel NewSchedule { get; set; } = new ScheduleViewModel();

        public IList<ScheduleViewModel> Schedules { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName; 
        }
    }
}
