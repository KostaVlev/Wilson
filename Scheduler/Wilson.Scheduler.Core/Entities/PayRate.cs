namespace Wilson.Scheduler.Core.Entities
{
    public class PayRate : Entity, IValueObject<PayRate>
    {
        public bool IsBaseRate { get; set; }

        public decimal Hour { get; set; }

        public decimal ExtraHour { get; set; }

        public decimal HoidayHour { get; set; }

        public decimal BusinessTripHour { get; set; }

        public bool Equals(PayRate other)
        {
            if (this.Hour == other.Hour && this.ExtraHour == other.ExtraHour &&
                this.HoidayHour == other.HoidayHour && this.BusinessTripHour == other.BusinessTripHour)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
