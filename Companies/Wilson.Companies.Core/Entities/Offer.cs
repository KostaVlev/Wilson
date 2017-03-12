using System;

namespace Wilson.Companies.Core.Entities
{
    public class Offer : Entity
    {
        public DateTime SentAt { get; set; }

        public int Revision { get; set; }

        public string HtmlContent { get; set; }

        public bool IsAccepted{ get; set; }

        public Guid InquiryId { get; set; }

        public Guid SentById { get; set; }

        public Guid ContractId { get; set; }

        public virtual Inquiry Inquiry { get; set; }

        public virtual Employee SentBy { get; set; }

        public virtual CompanyContract Contract { get; set; }
    }
}
