using System.Collections.Generic;
using Wilson.Scheduler.Core.Enumerations;

namespace Wilson.Scheduler.Core.Entities
{
    public class Employee : Entity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public EmployeePosition EmployeePosition { get; set; }

        public string PayRateId { get; set; }

        public virtual PayRate PayRate { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; } = new HashSet<Schedule>();

        public virtual ICollection<Paycheck> Paychecks { get; set; } = new HashSet<Paycheck>();
    }
}
