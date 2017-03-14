namespace Wilson.Scheduler.Core.Entities
{
    public class PayRate : Entity, IValueObject<PayRate>
    {
        public decimal Hour { get; set; }

        public decimal ExtraHour { get; set; }

        public decimal HoidayHour { get; set; }

        public bool Equals(PayRate other)
        {
            if (this.Hour == other.Hour && this.ExtraHour == other.ExtraHour)
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
