using System.ComponentModel.DataAnnotations;

namespace Wilson.Scheduler.Core.Enumerations
{
    public enum EmployeePosition
    {
        [Display(Name = "CEO")]
        CEO = 1,

        [Display(Name = "Manager")]
        Manager = 2,

        [Display(Name = "Office Staff")]
        OfficeSaff = 3,

        [Display(Name = "Skilled Worker")]
        SkilledWorker = 4,

        [Display(Name = "Worker")]
        Worker = 5
    }
}
