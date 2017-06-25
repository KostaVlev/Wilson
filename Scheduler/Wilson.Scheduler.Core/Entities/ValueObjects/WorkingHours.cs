using Newtonsoft.Json;
using System;

namespace Wilson.Scheduler.Core.Entities.ValueObjects
{
    [JsonObject]
    public class WorkingHours : ValueObject<WorkingHours>
    {
        protected WorkingHours()
        {
        }

        [JsonProperty]
        public int Hours { get; private set; }

        [JsonProperty]
        public int HourOnBusinessTrip { get; private set; }

        [JsonProperty]
        public int HourOnHolidays { get; private set; }

        [JsonProperty]
        public int ExtraHours { get; private set; }

        public static WorkingHours Create(int hours, int hoursOnBusinessTrip, int hoursOnHolidays, int extraHours)
        {
            if (hours > 8)
            {
                throw new ArgumentOutOfRangeException("hours", "Working hours can't excessed 8 hours.");
            }

            if (hoursOnBusinessTrip > 8)
            {
                throw new ArgumentOutOfRangeException("hoursOnBusinessTrip", "Working hours on Business Trip can't excessed 8 hours.");
            }

            if (extraHours > 6)
            {
                throw new ArgumentOutOfRangeException("extraHours", "Working extra hours can't excessed 6 hours.");
            }

            return new WorkingHours()
            {
                Hours = hours,
                HourOnBusinessTrip = hoursOnBusinessTrip,
                HourOnHolidays = hoursOnHolidays,
                ExtraHours = extraHours
            };
        }

        protected override bool EqualsCore(WorkingHours other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Hours == other.Hours && this.HourOnBusinessTrip == other.HourOnBusinessTrip &&
                   this.HourOnHolidays == other.HourOnHolidays && this.ExtraHours == this.ExtraHours;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = this.Hours.GetHashCode();
                hashCode = (hashCode * 397) ^ this.HourOnBusinessTrip.GetHashCode();
                hashCode = (hashCode * 397) ^ this.HourOnHolidays.GetHashCode();
                hashCode = (hashCode * 397) ^ this.ExtraHours.GetHashCode();

                return hashCode;
            }
        }
    }
}
