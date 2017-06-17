using Newtonsoft.Json;
using System;

namespace Wilson.Accounting.Core.Entities
{
    public class Payment : IValueObject<Payment>
    {
        private Payment()
        {
        }

        [JsonProperty]
        public DateTime Date { get; private set; }

        [JsonProperty]
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
