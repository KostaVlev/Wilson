using System;

namespace Wilson.Web.Areas.Scheduler.Models.SharedViewModels
{
    public class ScheduleViewModel
    {
        public DateTime Date { get; set; }

        public bool IsHoliday { get; set; }

        public bool IsPaidDayOff { get; set; }

        public bool IsUnpaidDayOff { get; set; }

        public bool IsSickDayOff { get; set; }

        public bool IsBusinessTrip { get; set; }

        public int WorkHours { get; set; }

        public int ExtraWorkHours { get; set; }

        public string ProjectId { get; set; }

        public ProjectViewModel Project { get; set; }
    }
}
