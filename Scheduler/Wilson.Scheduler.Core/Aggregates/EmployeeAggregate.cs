using System;
using Wilson.Scheduler.Core.Entities;

namespace Wilson.Scheduler.Core.Aggregates
{
    /// <summary>
    /// Provids way to schedule Employee work and generate <see cref="Paycheck"/>.
    /// </summary>
    public class EmployeeAggregate : Aggregate<Employee>
    {
        // TO DO Needs methods to be implemeted.

        /// <summary>
        /// Checks if the Invoice is valid.
        /// </summary>
        /// <returns>True if the <see cref="Employee"/> is valid.</returns>
        public override bool Validate()
        {
            // TODO Validate the Employee schedules and pays according the business rules which applies. 
            throw new NotImplementedException();
        }
    }
}
