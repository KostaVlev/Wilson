using System;
using System.Collections.Generic;

namespace Wilson.Companies.Core.Entities
{
    public class CompanyContract : Entity
    {
        public DateTime Date { get; set; }

        public int Revision { get; set; }

        public DateTime? LastRevisedAt { get; set; }

        public bool IsApproved { get; set; }

        public string HtmlContent { get; set; }

        public string ProjectId { get; set; }

        public string CretedById { get; set; }

        public virtual Project Project { get; set; }

        public virtual Employee CretedBy { get; set; }

        public virtual ICollection<Offer> Offers { get; set; } = new HashSet<Offer>();

        public virtual ICollection<Attachment> Attachments { get; set; } = new HashSet<Attachment>();
    }
}
