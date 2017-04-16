using System.ComponentModel.DataAnnotations;

namespace Wilson.Scheduler.Core.Enumerations
{
    public enum ScheduleOption
    {
        [Display(Name = "At Work")]
        AtWork = 0,

        [Display(Name = "Holiday")]
        Holiday = 1,

        [Display(Name = "Paid Day Off")]
        PaidDayOff = 2,

        [Display(Name = "Unpaid Day Off")]
        UnpaidDayOff = 3,

        [Display(Name = "Business Trip")]
        BusinessTrip = 4
    }
}
