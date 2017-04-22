using System.Collections.Generic;
using Wilson.Web.Areas.Scheduler.Models.HomeViewModels;
using Wilson.Web.Areas.Scheduler.Models.PayrollViewModels;

namespace Wilson.Web.Areas.Scheduler.Models.SharedViewModels
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            this.NewSchedule = new ScheduleViewModel()
            {
                Employee = new EmployeeConciseViewModel()
                {
                    Id = this.Id, FirstName = this.FirstName, LastName = this.LastName
                }
            };

        }

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ScheduleViewModel NewSchedule { get; set; }

        public PaycheckViewModel NewPaycheck { get; set; } = new PaycheckViewModel();

        public IList<ScheduleViewModel> Schedules { get; set; }

        public IEnumerable<PaycheckViewModel> Paychecks { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName; 
        }
    }
}
