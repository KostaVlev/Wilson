using System;
using System.Collections.Generic;

namespace Wilson.Companies.Core.Entities
{
    public class InfoRequest : Entity
    {
        public DateTime SentAt { get; set; }

        public DateTime? ResponseReceivedAt { get; set; }

        public string RequestMessage { get; set; }

        public string ResponseMessage { get; set; }

        public Guid InquiryId { get; set; }

        public Guid SentById { get; set; }

        public virtual Inquiry Inquiry { get; set; }

        public virtual Employee SentBy { get; set; }

        public virtual ICollection<Attachment> RequestAttachmnets { get; set; } = new HashSet<Attachment>();

        public virtual ICollection<Attachment> ResponseAttachmnets { get; set; } = new HashSet<Attachment>();
    }
}
