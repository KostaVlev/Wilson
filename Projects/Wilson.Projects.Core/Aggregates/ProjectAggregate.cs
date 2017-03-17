using System;
using Wilson.Projects.Core.Entities;

namespace Wilson.Projects.Core.Aggregates
{
    /// <summary>
    /// Provides way to manage <see cref="Invoice"/>.
    /// </summary>
    public class ProjectAggregate : Aggregate<Project>
    {
        // TO DO Needs methods to be implemeted.

        /// <summary>
        /// Checks if the Invoice is valid.
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            // TODO Validate the Invoice according the business rules which applies. 
            throw new NotImplementedException();
        }
    }
}
