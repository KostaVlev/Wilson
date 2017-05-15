using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Wilson.Companies.Core.Entities
{
    public class InfoRequest : Entity
    {
        private InfoRequest()
        {
        }

        public DateTime SentAt { get; private set; }

        public DateTime? ResponseReceivedAt { get; private set; }

        public string RequestMessage { get; private set; }

        public string ResponseMessage { get; private set; }

        public string InquiryId { get; private set; }

        public string SentById { get; private set; }

        public virtual Inquiry Inquiry { get; private set; }

        public virtual Employee SentBy { get; private set; }

        public virtual ICollection<Attachment> RequestAttachmnets { get; private set; }

        public virtual ICollection<Attachment> ResponseAttachmnets { get; private set; }

        public static InfoRequest Create(string requestMessage, Inquiry inquiry, Employee sentBy)
        {
            return new InfoRequest()
            {
                SentAt = DateTime.Now,
                RequestMessage = requestMessage,
                SentById = sentBy.Id,
                RequestAttachmnets = new HashSet<Attachment>(),
                ResponseAttachmnets = new HashSet<Attachment>()
            };
        }

        public void ReceiveResponse(string responseMessage, IEnumerable<IFormFile> files = null)
        {
            this.ResponseMessage = responseMessage;
            if (files != null)
            {
                this.AddAttachments(files, true);
            }
        }

        public void AddAttachment(IFormFile file, bool isFromResponse = true)
        {
            Attachment attachment;
            if (isFromResponse)
            {
                attachment = Attachment.Create(file, infoRequestResponse: this);                
            }
            else
            {
                attachment = Attachment.Create(file, infoRequest: this);                
            }

            this.RequestAttachmnets.Add(attachment);
        }

        public void AddAttachments(IEnumerable<IFormFile> files, bool areFromResponse)
        {
            if (areFromResponse)
            {
                foreach (var file in files)
                {
                    var attachment = Attachment.Create(file, infoRequestResponse: this);
                    this.RequestAttachmnets.Add(attachment);
                }
            }
            else
            {
                foreach (var file in files)
                {
                    var attachment = Attachment.Create(file, infoRequest: this);
                    this.RequestAttachmnets.Add(attachment);
                }
            }
        }
    }
}
