using System;

namespace Wilson.Accounting.Core.Entities
{
    public class Payment : IValueObject<Payment>
    {
        public DateTime Date { get; private set; }

        public decimal Amount { get; private set; }

        public static Payment Create(DateTime date, decimal amount)
        {
            return new Payment() { Date = date, Amount = amount };
        }

        public bool Equals(Payment other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Date == other.Date && this.Amount == other.Amount;
        }
    }
}
