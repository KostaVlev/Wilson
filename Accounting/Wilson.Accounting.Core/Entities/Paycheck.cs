using System;

namespace Wilson.Accounting.Core.Entities
{
    public class Paycheck : Entity, IValueObject<Paycheck>
    {
        private Paycheck()
        {
        }

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

        public bool IsPaied { get; private set; }

        public string EmployeeId { get; private set; }

        public virtual Employee Employee { get; private set; }

        public string Payments { get; private set; }

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
            
            this.Payments = this.GetPayments().Add(Payment.Create(date, amount));
        }

        public decimal GetPaidAmount()
        {
            if (string.IsNullOrEmpty(this.Payments))
            {
                return 0;
            }

            return this.GetPayments().Sum();
        }

        public ListOfPayments GetPayments()
        {
            if (string.IsNullOrEmpty(this.Payments))
            {
                return ListOfPayments.Create();
            }

            return (ListOfPayments)this.Payments;
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
