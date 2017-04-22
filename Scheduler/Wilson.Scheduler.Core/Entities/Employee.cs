using System;
using System.Collections.Generic;
using Wilson.Scheduler.Core.Enumerations;

namespace Wilson.Scheduler.Core.Entities
{
    public class Employee : Entity
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public EmployeePosition EmployeePosition { get; private set; }

        public string PayRateId { get; private set; }

        public virtual PayRate PayRate { get; private set; }

        public virtual ICollection<Schedule> Schedules { get; private set; } = new HashSet<Schedule>();

        public virtual ICollection<Paycheck> Paychecks { get; private set; } = new HashSet<Paycheck>();

        public static Employee Create(string firstName, string lastName, PayRate payRate, EmployeePosition employeePosition)
        {
            
            var employee =  new Employee()
            {
                FirstName = firstName,
                LastName = lastName,
                PayRateId = payRate.Id,
                PayRate = payRate
            };

            employee.AssighnEmployeePossition(employeePosition);

            return employee;
        }

        public void AddNewPaycheck(DateTime issueDate)
        {
            var paycheck = Paycheck.Create(this, DateTime.Now);
            if (!this.Paychecks.Contains(paycheck))
            {
                this.Paychecks.Add(paycheck);                
            }
        }        

        public void ApplayPayRate(PayRate payRate)
        {
            this.PayRateId = PayRateId;
        }

        public void AssighnEmployeePossition(EmployeePosition employeePosition)
        {
            this.EmployeePosition = employeePosition;
        }

        public void AddNewSchedule(Schedule schedule)
        {
            if (schedule.EmployeeId != this.Id)
            {
                throw new ArgumentException(
                    "The Schedule doesn't belong to the employee. Check the schedule.EmployeeId property.", 
                    "schedule");
            }

            var newSchedule = Schedule.Create(schedule.Date, schedule.ScheduleOption, schedule.EmployeeId, schedule.ProjectId);
            if (this.Schedules.Contains(newSchedule))
            {
                throw new ArgumentException("Only one Schedule can be added per date for an employee");
            }

            this.Schedules.Add(newSchedule);
        }

        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}
