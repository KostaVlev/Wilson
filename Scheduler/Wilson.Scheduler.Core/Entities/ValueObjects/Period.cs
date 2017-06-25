using Newtonsoft.Json;
using System;

namespace Wilson.Scheduler.Core.Entities.ValueObjects
{
    [JsonObject]
    public class Period : ValueObject<Period>
    {
        protected Period()
        {
        }

        [JsonProperty]
        public DateTime From { get; private set; }

        [JsonProperty]
        public DateTime To { get; private set; }

        public static Period Create(DateTime? from = null, DateTime? to = null)
        {
            if (!from.HasValue || !to.HasValue)
            {
                return GetDefaultPeriod();
            }

            if (from > to)
            {
                throw new ArgumentException("Invalid time period. The date To can't be prior the from date.");
            }

            return new Period() { From = from.Value, To = to.Value };
        }

        public static Period CreateForCurrentMonth(DateTime date)
        {     
            return new Period() { From = GetFirstDayOfMonth(date), To = GetLastDayOfMonth(date) };
        }

        private static DateTime GetFirstDayOfMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        private static DateTime GetLastDayOfMonth(DateTime firstDayOfTheMonth)
        {
            return firstDayOfTheMonth.AddMonths(1).AddDays(-1).AddTicks(-1);
        }

        private static Period GetDefaultPeriod()
        {
            var today = DateTime.Today;
            return new Period() { From = today.AddMonths(-1), To = today };
        }

        protected override bool EqualsCore(Period other)
        {
            if (other == null)
            {
                return false;
            }

            return this.From == other.From && this.To == other.To;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = this.From.GetHashCode();
                hashCode = (hashCode * 397) ^ this.To.GetHashCode();

                return hashCode;
            }
        }
    }
}
