using System;

namespace Wilson.Scheduler.Core.Entities
{
    public class Paycheck : Entity
    {
        public DateTime Date { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public int Hours { get; set; }

        public int HourOnHolidays { get; set; }

        public int ExtraHours { get; set; }

        public int PaidDaysOff { get; set; }

        public int UnpaidDaysOff { get; set; }

        public int SickDaysOff { get; set; }

        public decimal PayForHours { get; set; }

        public decimal PayForExtraHours { get; set; }

        public decimal PayForHolidayHours { get; set; }

        public decimal PayForPayedDaysOff { get; set; }

        public decimal Total { get; set; }

        public Guid EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
