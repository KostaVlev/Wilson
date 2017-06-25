using System;
using System.Collections.Generic;
using System.Linq;
using Wilson.Scheduler.Core.Entities.ValueObjects;
using Wilson.Scheduler.Core.Enumerations;

namespace Wilson.Scheduler.Core.Entities
{
    public class Paycheck : Entity
    {
        public DateTime Date { get; private set; }

        public string Period { get; private set; }

        public string WorkingHours { get; private set; }

        public string DaysOff { get; private set; }

        public string SubTotals { get; private set; }

        public decimal Total { get; private set; }

        public string EmployeeId { get; private set; }

        public virtual Employee Employee { get; private set; }

        public static Paycheck Create(Employee employee, DateTime issuingDate, DateTime from)
        {
            var period = ValueObjects.Period.CreateForCurrentMonth(from);
            var paycheck = new Paycheck()
            {
                EmployeeId = employee.Id,
                Date = issuingDate,
                Period = period,
            };

            CalculateHours(paycheck, employee.Schedules.Where(s => 
                s.Date >= period.From && s.Date <= period.To));

            paycheck.SubTotals = CalculateSubTotals(paycheck.GetWorkingHours(), paycheck.GetDaysOff(), employee.PayRate);
            paycheck.Total = CalculateTotal(paycheck.GetSubTotals());

            return paycheck;
        }

        public void Update(Employee employee)
        {
            if (this.EmployeeId != employee.Id)
            {
                throw new ArgumentException("The old paycheck does not belong to the employee.");
            }

            CalculateHours(this, employee.Schedules.Where(s =>
                s.Date >= this.GetPeriod().From && s.Date <= this.GetPeriod().To));

            this.SubTotals = CalculateSubTotals(this.GetWorkingHours(), this.GetDaysOff(), employee.PayRate);
            this.Total = CalculateTotal(this.GetSubTotals());
        }

        private static void CalculateHours(Paycheck paycheck, IEnumerable<Schedule> schedules)
        {
            var paidDaysOff = schedules.Where(x => x.ScheduleOption == ScheduleOption.PaidDayOff).Count();
            var unpaidDaysOff = schedules.Where(x => x.ScheduleOption == ScheduleOption.UnpaidDayOff).Count();
            var sickDaysOff = schedules.Where(x => x.ScheduleOption == ScheduleOption.UnpaidDayOff).Count();

            paycheck.DaysOff = ValueObjects.DaysOff.Create(paidDaysOff, unpaidDaysOff, sickDaysOff);

            var hourOnBusinessTrip = schedules.Where(x => x.ScheduleOption == ScheduleOption.BusinessTrip).Sum(x => x.WorkHours);
            var hourOnHolidays = schedules.Where(x => x.ScheduleOption == ScheduleOption.Holiday).Sum(x => x.WorkHours);
            var hours = schedules.Where(x => x.ScheduleOption == ScheduleOption.AtWork).Sum(x => x.WorkHours);
            var extraHours = schedules.Where(x =>
                x.ScheduleOption == ScheduleOption.Holiday ||
                x.ScheduleOption == ScheduleOption.AtWork ||
                x.ScheduleOption == ScheduleOption.BusinessTrip)
                .Sum(x => x.ExtraWorkHours);

            paycheck.WorkingHours = ValueObjects.WorkingHours.Create(hours, hourOnBusinessTrip, hourOnHolidays, extraHours);
        }

        private static SubTotals CalculateSubTotals(WorkingHours workingHours, DaysOff daysOff, PayRate payRate)
        {
            var payForHours = workingHours.Hours * payRate.Hour;
            var payForBusinessTrip = workingHours.HourOnBusinessTrip * payRate.BusinessTripHour;
            var payForExtraHours = workingHours.ExtraHours * payRate.ExtraHour;
            var payForHolidayHours = workingHours.HourOnHolidays * payRate.HoidayHour;
            var payForPayedDaysOff = daysOff.PaidDaysOff * (payRate.Hour * 8);

            return ValueObjects.SubTotals.Create(payForHours, payForBusinessTrip, payForExtraHours, payForHolidayHours, payForPayedDaysOff);
        }

        private static decimal CalculateTotal(SubTotals subTotals)
        {
            return subTotals.Sum();
        }

        public Period GetPeriod()
        {
            return (Period)this.Period;
        }

        public WorkingHours GetWorkingHours()
        {
            return (WorkingHours)this.WorkingHours;
        }

        public SubTotals GetSubTotals()
        {
            return (SubTotals)this.SubTotals;
        }

        public DaysOff GetDaysOff()
        {
            return (DaysOff)this.DaysOff;
        }        
    }
}
