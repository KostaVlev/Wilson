using Newtonsoft.Json;

namespace Wilson.Scheduler.Core.Entities.ValueObjects
{
    [JsonObject]
    public class SubTotals : ValueObject<SubTotals>
    {
        protected SubTotals()
        {
        }

        [JsonProperty]
        public decimal PayForHours { get; private set; }

        [JsonProperty]
        public decimal PayForBusinessTrip { get; private set; }

        [JsonProperty]
        public decimal PayForExtraHours { get; private set; }

        [JsonProperty]
        public decimal PayForHolidayHours { get; private set; }

        [JsonProperty]
        public decimal PayForPayedDaysOff { get; private set; }

        public static SubTotals Create(
            decimal payForHours, decimal payForBusinessTrip, decimal payForExtraHours, decimal payForHolidayHours, decimal payForPayedDaysOff)
        {
            return new SubTotals()
            {
                PayForHours = payForHours,
                PayForBusinessTrip = payForBusinessTrip,
                PayForExtraHours = payForExtraHours,
                PayForHolidayHours = payForHolidayHours,
                PayForPayedDaysOff = payForPayedDaysOff
            };
        }

        public decimal Sum()
        {
            return this.PayForHours + this.PayForBusinessTrip + this.PayForExtraHours + this.PayForHolidayHours + this.PayForPayedDaysOff;
        }

        protected override bool EqualsCore(SubTotals other)
        {
            if (other == null)
            {
                return false;
            }

            return this.PayForHours == other.PayForHours && this.PayForPayedDaysOff == other.PayForPayedDaysOff &&
                   this.PayForExtraHours == other.PayForExtraHours && this.PayForHolidayHours == other.PayForHolidayHours &&
                   this.PayForPayedDaysOff == other.PayForPayedDaysOff;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = this.PayForHours.GetHashCode();
                hashCode = (hashCode * 397) ^ this.PayForBusinessTrip.GetHashCode();
                hashCode = (hashCode * 397) ^ this.PayForExtraHours.GetHashCode();
                hashCode = (hashCode * 397) ^ this.PayForHolidayHours.GetHashCode();
                hashCode = (hashCode * 397) ^ this.PayForPayedDaysOff.GetHashCode();

                return hashCode;
            }
        }
    }
}
