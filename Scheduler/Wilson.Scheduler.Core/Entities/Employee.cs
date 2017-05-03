using System;
using System.Collections.Generic;
using Wilson.Scheduler.Core.Enumerations;

namespace Wilson.Scheduler.Core.Entities
{
    public class Employee : Entity
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public bool IsFired { get; private set; }

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
                PayRate = payRate,
                IsFired = false
            };

            employee.AssighnEmployeePossition(employeePosition);

            return employee;
        }

        public void AddNewPaycheck(DateTime issueDate, DateTime from)
        {
            var paycheck = Paycheck.Create(this, DateTime.Now, from);
            if (!this.Paychecks.Contains(paycheck))
            {
                this.Paychecks.Add(paycheck);                
            }
        }        

        public void ApplayPayRate(PayRate payRate)
        {
            this.PayRate = payRate;
            this.PayRateId = PayRateId;
        }

        public void AssighnEmployeePossition(EmployeePosition employeePosition)
        {
            this.EmployeePosition = employeePosition;
        }

        public void AddNewSchedule(Schedule schedule)
        {
            var newSchedule = Schedule.Create(DateTime.Now, schedule.ScheduleOption, this.Id, schedule.ProjectId);
            if (this.Schedules.Contains(newSchedule))
            {
                throw new ArgumentException("Only one Schedule can be added per date for an employee");
            }

            this.Schedules.Add(newSchedule);
        }

        public void Fire()
        {
            this.IsFired = true;
        }

        public void Hire()
        {
            this.IsFired = false;
        }

        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}
