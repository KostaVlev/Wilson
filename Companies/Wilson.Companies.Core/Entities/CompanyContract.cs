using System;
using System.Collections.Generic;

namespace Wilson.Companies.Core.Entities
{
    public class CompanyContract : Entity
    {
        public DateTime Date { get; set; }

        public string HtmlContent { get; set; }

        public Guid ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public virtual ICollection<Offer> Offers { get; set; } = new HashSet<Offer>();

        public virtual ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();
    }
}
