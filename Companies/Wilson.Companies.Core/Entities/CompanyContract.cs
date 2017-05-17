using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Wilson.Companies.Core.Entities
{
    public class CompanyContract : Entity
    {
        private CompanyContract()
        {
        }

        public DateTime Date { get; private set; }

        public int Revision { get; private set; }

        public DateTime? LastRevisedAt { get; private set; }

        public bool IsApproved { get; private set; }

        public string HtmlContent { get; private set; }

        public string ProjectId { get; private set; }

        public string CretedById { get; private set; }

        public string RevisedById { get; private set; }

        public virtual Project Project { get; private set; }

        public virtual Employee CretedBy { get; private set; }

        public virtual Employee RevisedBy { get; private set; }

        public virtual ICollection<Offer> Offers { get; private set; }

        public virtual ICollection<Attachment> Attachments { get; private set; }

        public static CompanyContract Create(string htmlContent, Employee createdBy, Project project)
        {
            return new CompanyContract()
            {
                Date = DateTime.Now,
                CretedById = createdBy.Id,
                ProjectId = project.Id,
                Attachments = new HashSet<Attachment>(),
                Offers = new HashSet<Offer>()
            };
        }

        public void SetContractSigningDate(DateTime date)
        {
            this.Date = date;
        }

        public void Revise(string htmlContent, Employee revisedBy)
        {
            this.HtmlContent = htmlContent;
            this.RevisedById = revisedBy.Id;
            this.LastRevisedAt = DateTime.Now;
            this.Revision++;
        }

        public void AddOffer(Offer offer)
        {
            if (!this.Offers.Contains(offer))
            {
                offer.AddToContract(this.Id);
                this.Offers.Add(offer);
            }
        }

        public void AddAttachment(IFormFile file)
        {
            var attachment = Attachment.Create(file, contract: this);
            this.Attachments.Add(attachment);
        }

        public void AddAttachments(IEnumerable<IFormFile> files)
        {
            foreach (var file in files)
            {
                var attachment = Attachment.Create(file, contract: this);
                this.Attachments.Add(attachment);
            }
        }
    }
}
