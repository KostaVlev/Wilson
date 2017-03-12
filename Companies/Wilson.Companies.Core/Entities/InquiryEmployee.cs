using System;

namespace Wilson.Companies.Core.Entities
{
    public class InquiryEmployee : IEntity
    {
        public Guid InquiryId { get; set; }

        public Guid EmployeeId { get; set; }

        public virtual Inquiry Inquiry { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
