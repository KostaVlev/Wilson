using System;
using Wilson.Scheduler.Core.Enumerations;

namespace Wilson.Scheduler.Core.Entities
{
    public class Schedule : Entity
    {
        public DateTime Date { get; set; }

        public ScheduleOption ScheduleOption { get; set; }

        public int WorkHours { get; set; }

        public int ExtraWorkHours { get; set; }

        public string EmployeeId { get; set; }

        public string ProjectId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Project Project { get; set; }
    }
}
