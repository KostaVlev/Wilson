namespace Wilson.Scheduler.Core.Entities.ValueObjects
{
    public class DaysOff : ValueObject<DaysOff>
    {
        protected DaysOff()
        {
        }

        public int PaidDaysOff { get; private set; }

        public int UnpaidDaysOff { get; private set; }

        public int SickDaysOff { get; private set; }

        public static DaysOff Create(int paidDaysOff = 0, int unpaidDaysOff = 0, int sickDaysOff = 0)
        {
            return new DaysOff() { PaidDaysOff = paidDaysOff, UnpaidDaysOff = unpaidDaysOff, SickDaysOff = sickDaysOff };
        }

        protected override bool EqualsCore(DaysOff other)
        {
            if (other == null)
            {
                return false;
            }

            return this.PaidDaysOff == other.PaidDaysOff && 
                   this.UnpaidDaysOff == other.UnpaidDaysOff && 
                   this.SickDaysOff == other.SickDaysOff;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = this.PaidDaysOff.GetHashCode();
                hashCode = (hashCode * 397) ^ this.UnpaidDaysOff.GetHashCode();
                hashCode = (hashCode * 397) ^ this.SickDaysOff.GetHashCode();

                return hashCode;
            }
        }
    }
}
