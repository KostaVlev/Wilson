using System;
using System.Collections.Generic;
using System.Linq;
using Wilson.Scheduler.Core.Enumerations;

namespace Wilson.Scheduler.Core.Entities
{
    public class Paycheck : Entity, IValueObject<Paycheck>
    {
        public DateTime Date { get; private set; }

        public DateTime From { get; private set; }

        public DateTime To { get; private set; }

        public int Hours { get; private set; }

        public int HourOnBusinessTrip { get; private set; }

        public int HourOnHolidays { get; private set; }

        public int ExtraHours { get; private set; }

        public int PaidDaysOff { get; private set; }

        public int UnpaidDaysOff { get; private set; }

        public int SickDaysOff { get; private set; }

        public decimal PayForHours { get; private set; }

        public decimal PayBusinessTrip { get; private set; }

        public decimal PayForExtraHours { get; private set; }

        public decimal PayForHolidayHours { get; private set; }

        public decimal PayForPayedDaysOff { get; private set; }

        public decimal Total { get; private set; }

        public string EmployeeId { get; private set; }

        public virtual Employee Employee { get; private set; }

        public static Paycheck Create(Employee employee, DateTime issuingDate, DateTime from)
        {            
            var firstDayOfMonth = GetFirstDayOfMonth(from);
            var lastDayOfMonth = GetLastDayOfMonth(firstDayOfMonth);
            var paycheck = new Paycheck()
            {
                EmployeeId = employee.Id,
                Date = issuingDate,
                From = firstDayOfMonth,
                To = lastDayOfMonth
            };

            CalculateHours(paycheck, employee.Schedules.Where(s => 
                s.Date >= firstDayOfMonth && s.Date <= lastDayOfMonth));

            CalculatePayments(paycheck, employee.PayRate);

            return paycheck;
        }

        private static void CalculateHours(Paycheck paycheck, IEnumerable<Schedule> schedules)
        {
            paycheck.PaidDaysOff = schedules.Where(x => x.ScheduleOption == ScheduleOption.PaidDayOff).Count();
            paycheck.UnpaidDaysOff = schedules.Where(x => x.ScheduleOption == ScheduleOption.UnpaidDayOff).Count();
            paycheck.SickDaysOff = schedules.Where(x => x.ScheduleOption == ScheduleOption.UnpaidDayOff).Count();
            paycheck.HourOnBusinessTrip = schedules.Where(x => x.ScheduleOption == ScheduleOption.BusinessTrip).Sum(x => x.WorkHours);
            paycheck.HourOnHolidays = schedules.Where(x => x.ScheduleOption == ScheduleOption.Holiday).Sum(x => x.WorkHours);
            paycheck.Hours = schedules.Where(x => x.ScheduleOption == ScheduleOption.AtWork).Sum(x => x.WorkHours);
            paycheck.ExtraHours = schedules.Where(x =>
                x.ScheduleOption == ScheduleOption.Holiday ||
                x.ScheduleOption == ScheduleOption.AtWork ||
                x.ScheduleOption == ScheduleOption.BusinessTrip)
                .Sum(x => x.ExtraWorkHours);
        }

        private static void CalculatePayments(Paycheck paycheck, PayRate payRate)
        {
            paycheck.PayForPayedDaysOff = paycheck.PaidDaysOff * 8 * payRate.Hour;
            paycheck.PayBusinessTrip = paycheck.HourOnBusinessTrip * payRate.BusinessTripHour;
            paycheck.PayForHolidayHours = paycheck.HourOnHolidays * payRate.HoidayHour;
            paycheck.PayForHours = paycheck.Hours * payRate.HoidayHour;
            paycheck.PayForExtraHours = paycheck.ExtraHours * payRate.ExtraHour;

            paycheck.Total = paycheck.PayForPayedDaysOff + paycheck.PayBusinessTrip + paycheck.PayForHolidayHours + paycheck.PayForHours + paycheck.PayForExtraHours;
        }

        private static DateTime GetFirstDayOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        private static DateTime GetLastDayOfMonth(DateTime firstDayOfTheMonth)
        {
            return firstDayOfTheMonth.AddMonths(1).AddDays(-1).AddTicks(-1);
        }

        public bool Equals(Paycheck other)
        {
            if (other == null)
            {
                return false;
            }

            return this.From == other.From && this.To == other.To && this.EmployeeId == other.EmployeeId;
        }
    }
}
