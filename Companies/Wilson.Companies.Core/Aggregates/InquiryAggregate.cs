using System;
using Wilson.Companies.Core.Entities;

namespace Wilson.Companies.Core.Aggregates
{
    /// <summary>
    /// Provides way to manage <see cref="Invoice"/>.
    /// </summary>
    public class InquiryAggregate : Aggregate<Inquiry>
    {
        private Inquiry Inquiry
        {
            get { return this.Entity as Inquiry; }
        }

        

        /// <summary>
        /// Checks if the Invoice is valid.
        /// </summary>
        /// <returns>Returns true if the <see cref="Inquiry"/> is valid, otherwise false.</returns>
        public override bool Validate()
        {
            // TODO Validate the Inquiry according the business rules which applies. 
            throw new NotImplementedException();
        }
    }
}
