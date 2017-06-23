using Newtonsoft.Json;
using System;

namespace Wilson.Accounting.Core.Entities
{
    [JsonObject]
    public class Payment : ValueObject<Payment>
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

        protected override bool EqualsCore(Payment other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Date == other.Date && this.Amount == other.Amount;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = Date.GetHashCode();
                hashCode = (hashCode * 397) ^ Amount.GetHashCode();

                return hashCode;
            }
        }
    }
}
