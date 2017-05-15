using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Wilson.Companies.Core.Entities
{
    public class Inquiry : Entity
    {
        private Inquiry()
        {
        }

        public DateTime ReceivedAt { get; private set; }

        public DateTime? ClosedAt { get; private set; }

        public string Description { get; private set; }

        public string ReceivedById { get; private set; }

        public string CustomerId { get; private set; }

        public virtual Employee ReceivedBy { get; private set; }

        public virtual Company Customer { get; private set; }

        public virtual ICollection<Attachment> Attachments { get; private set; }

        public virtual ICollection<InfoRequest> InfoRequests { get; private set; }

        public virtual ICollection<InquiryEmployee> Assignees { get; private set; }

        public virtual ICollection<Offer> Offers { get; private set; }

        public static Inquiry Create(string description, Employee receivedBy, string customerId)
        {
            return new Inquiry()
            {
                ReceivedAt = DateTime.Now,
                Description = description,
                ReceivedById = receivedBy.Id,
                CustomerId = customerId,
                Attachments = new HashSet<Attachment>(),
                InfoRequests = new HashSet<InfoRequest>(),
                Assignees = new HashSet<InquiryEmployee>(),
                Offers = new HashSet<Offer>()
            };
        }

        public void Close()
        {
            this.ClosedAt = DateTime.Now;
        }

        public void ReOpen()
        {
            this.ClosedAt = null;
        }

        public void AddAttachment(IFormFile file)
        {
            var attachment = Attachment.Create(file, inquiry: this);
            this.Attachments.Add(attachment);
        }

        public void AddAttachments(IEnumerable<IFormFile> files)
        {
            foreach (var file in files)
            {
                var attachment = Attachment.Create(file, inquiry: this);
                this.Attachments.Add(attachment);
            }
        }

        public void AddInfoRequest(string requestMessage, Employee sendBy, IEnumerable<IFormFile> files = null)
        {
            var infoRequest = InfoRequest.Create(requestMessage, this, sendBy);
            if (files != null)
            {
                infoRequest.AddAttachments(files, false);
            }

            this.InfoRequests.Add(infoRequest);
        }

        public void AddAssignee(string assigneeId)
        {
            var newAssignee = InquiryEmployee.Create(assigneeId, this.Id);
            if (!this.Assignees.Contains(newAssignee))
            {
                this.Assignees.Add(newAssignee);
            }
        }

        public void AddAssignees(IEnumerable<string> assigneeIds)
        {
            foreach (var id in assigneeIds)
            {
                this.AddAssignee(id);
            }
        }

        public void AddOffer(string htmlContent, Employee addedBy)
        {
            var offer = Offer.Create(htmlContent, this, addedBy);
            this.Offers.Add(offer);
        }
    }
}
