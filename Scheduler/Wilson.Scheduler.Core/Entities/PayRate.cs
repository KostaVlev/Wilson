namespace Wilson.Scheduler.Core.Entities
{
    public class PayRate : Entity, IValueObject<PayRate>
    {
        public bool IsBaseRate { get; private set; }

        public decimal Hour { get; private set; }

        public decimal ExtraHour { get; private set; }

        public decimal HoidayHour { get; private set; }

        public decimal BusinessTripHour { get; private set; }

        public static PayRate Create(decimal hourRate, decimal extraHourRate, decimal holidayHourRate, decimal businessTripHourRate, bool isBaseRate = false)
        {
            return new PayRate()
            {
                IsBaseRate = isBaseRate,
                Hour = hourRate,
                ExtraHour = extraHourRate,
                HoidayHour = holidayHourRate,
                BusinessTripHour = businessTripHourRate
            };
        }

        public bool Equals(PayRate other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Hour == other.Hour && this.ExtraHour == other.ExtraHour &&
                this.HoidayHour == other.HoidayHour && this.BusinessTripHour == other.BusinessTripHour;
        }
    }
}
