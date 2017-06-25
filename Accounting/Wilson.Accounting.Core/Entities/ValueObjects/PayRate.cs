namespace Wilson.Accounting.Core.Entities.ValueObjects
{
    public class PayRate : ValueObject<PayRate>
    {
        protected PayRate()
        {
        }

        public decimal HourRate { get; private set; }

        public decimal ExtraHourRate { get; private set; }

        public decimal HoidayHourRate { get; private set; }

        public decimal BusinessTripHourRate { get; private set; }

        public static PayRate Create(decimal hourRate, decimal extraHourRate, decimal holidayHourRate, decimal businessTripHourRate)
        {
            return new PayRate()
            {
                HourRate = hourRate,
                ExtraHourRate = extraHourRate,
                HoidayHourRate = holidayHourRate,
                BusinessTripHourRate = businessTripHourRate
            };
        }

        protected override bool EqualsCore(PayRate other)
        {
            if (other == null)
            {
                return false;
            }

            return this.HourRate == other.HourRate && this.ExtraHourRate == other.ExtraHourRate &&
                   this.HoidayHourRate == other.HoidayHourRate && this.BusinessTripHourRate == other.BusinessTripHourRate;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = this.HourRate.GetHashCode();
                hashCode = (hashCode * 397) ^ this.ExtraHourRate.GetHashCode();
                hashCode = (hashCode * 397) ^ this.HoidayHourRate.GetHashCode();
                hashCode = (hashCode * 397) ^ this.BusinessTripHourRate.GetHashCode();

                return hashCode;
            }
        }
    }
}
