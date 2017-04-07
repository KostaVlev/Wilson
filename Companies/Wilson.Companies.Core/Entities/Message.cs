using System;
using Wilson.Companies.Core.Enumerations;

namespace Wilson.Companies.Core.Entities
{
    public class Message : Entity
    {
        public string Subject { get; set; }

        public MessageCategory MessageCategory { get; set; }

        public string Body { get; set; }

        public DateTime SentAt { get; set; }

        public DateTime? RecivedAt { get; set; }

        public bool IsNew { get; set; }

        public bool IsDeleted { get; set; }

        public string SenderId { get; set; }

        public string RecipientId { get; set; }

        public virtual User Sender { get; set; }

        public virtual User Recipient { get; set; }
    }
}
