using System;
using Wilson.Accounting.Core.Entities.ValueObjects;

namespace Wilson.Accounting.Core.Entities
{
    public class Paycheck : Entity
    {
        protected Paycheck()
        {
        }

        public DateTime Date { get; private set; }

        public string Period { get; private set; }

        public string WorkingHours { get; private set; }

        public string DaysOff { get; private set; }

        public string SubTotals { get; private set; }

        public string PayRate { get; private set; }

        public decimal Total { get; private set; }

        public string Payments { get; private set; }

        public bool IsPaied { get; private set; }

        public string EmployeeId { get; private set; }

        public virtual Employee Employee { get; private set; }

        public static Paycheck Create(
             DateTime date, Period period, WorkingHours workingHours, DaysOff daysOff, PayRate payRate, string employeeId)
        {
            var subTotal = CalculateSubTotals(workingHours, daysOff, payRate);
            return new Paycheck()
            {
                Date = date,
                Period = period,
                WorkingHours = workingHours,
                DaysOff = daysOff,
                PayRate = payRate,
                SubTotals = subTotal,
                Total = CalculateTotal(subTotal),
                IsPaied = false,
                EmployeeId = employeeId
            };
        }

        public void AddPayment(DateTime date, decimal amount)
        {
            decimal paidAmount = this.GetPaidAmount();
            decimal diffrenceToPay = this.Total - paidAmount;
            if (diffrenceToPay == amount)
            {
                this.IsPaied = true;
            }

            if (diffrenceToPay < amount)
            {
                throw new ArgumentOutOfRangeException("amount", "Payment amount can't be more then the amount that have to be paid.");
            }

            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount", "Payment amount can't be negative number.");
            }

            // Add new Payment only if the amount is greater then zero.
            if (amount > 0)
            {
                this.Payments = this.GetPayments().Add(Payment.Create(date, amount));
            }
        }

        public decimal GetPaidAmount()
        {
            return this.GetPayments().Sum();
        }

        public ListOfPayments GetPayments()
        {
            return string.IsNullOrEmpty(this.Payments) ? ListOfPayments.Create() : (ListOfPayments)this.Payments;
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

        public PayRate GetPayRate()
        {
            return (PayRate)this.PayRate;
        }

        private static SubTotals CalculateSubTotals(WorkingHours workingHours, DaysOff daysOff, PayRate payRate)
        {
            var payForHours = workingHours.Hours * payRate.HourRate;
            var payForBusinessTrip = workingHours.HourOnBusinessTrip * payRate.BusinessTripHourRate;
            var payForExtraHours = workingHours.ExtraHours * payRate.ExtraHourRate;
            var payForHolidayHours = workingHours.HourOnHolidays * payRate.HoidayHourRate;
            var payForPayedDaysOff = daysOff.PaidDaysOff * (payRate.HourRate * 8);

            return ValueObjects.SubTotals.Create(payForHours, payForBusinessTrip, payForExtraHours, payForHolidayHours, payForPayedDaysOff);
        }

        private static decimal CalculateTotal(SubTotals subTotals)
        {
            return subTotals.Sum();
        }
    }
} 
