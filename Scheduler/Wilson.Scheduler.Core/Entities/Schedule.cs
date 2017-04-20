using System;
using Wilson.Scheduler.Core.Enumerations;

namespace Wilson.Scheduler.Core.Entities
{
    public class Schedule : Entity, IValueObject<Schedule>
    {
        public DateTime Date { get; private set; }

        public ScheduleOption ScheduleOption { get; private set; }

        public int WorkHours { get; private set; }

        public int ExtraWorkHours { get; private set; }

        public string EmployeeId { get; private set; }

        public string ProjectId { get; private set; }

        public virtual Employee Employee { get; private set; }

        public virtual Project Project { get; private set; }

        public static Schedule Create(
            DateTime date, ScheduleOption scheduleOption, string employeeId, 
            string projectId, int workHours = 8, int extraWorkHours = 0)
        {            

            if (scheduleOption != ScheduleOption.AtWork)
            {
                projectId = null;
            }

            var schedule = new Schedule()
            {
                Date = date, ScheduleOption = scheduleOption, EmployeeId = employeeId,
                ProjectId = projectId, WorkHours = workHours, ExtraWorkHours = extraWorkHours
            };

            Validate(schedule);

            return schedule;
        }

        public void Update(int workHours, int extraWorkHours, ScheduleOption scheduleOption, string projectId)
        {
            this.WorkHours = workHours;
            this.ExtraWorkHours = extraWorkHours;            
            this.ScheduleOption = scheduleOption;
            this.ProjectId = projectId;
        }

        private static void Validate(Schedule schedule)
        {
            if (schedule.WorkHours < 2 || schedule.WorkHours > 8)
            {
                throw new ArgumentException("Work hours cannot be less the 2 or more then 8.", "workHours");
            }

            if (schedule.ExtraWorkHours < 0 || schedule.ExtraWorkHours > 4)
            {
                throw new ArgumentException("Extra work hours cannot be less the 0 or more then 4.", "extraWorkHours");
            }
        }

        public bool Equals(Schedule other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Date.ToString("dd-MMM-yyyy") == other.Date.ToString("dd-MMM-yyyy");
        }
    }
}
