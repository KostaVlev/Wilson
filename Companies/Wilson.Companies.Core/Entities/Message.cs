using System;
using Wilson.Companies.Core.Enumerations;

namespace Wilson.Companies.Core.Entities
{
    public class Message : Entity
    {
        public string Subject { get; private set; }

        public MessageCategory MessageCategory { get; private set; }

        public string Body { get; private set; }

        public DateTime SentAt { get; private set; }

        public DateTime? RecivedAt { get; private set; }

        public bool IsNew { get; private set; }

        public bool IsDeleted { get; private set; }

        public bool IsRecived { get; private set; }

        public string SenderId { get; private set; }

        public string RecipientId { get; private set; }

        public virtual ApplicationUser Sender { get; private set; }

        public virtual ApplicationUser Recipient { get; private set; }

        public static Message Create(string subject, string message, MessageCategory category, ApplicationUser sender, ApplicationUser recipient)
        {
            return new Message()
            {
                Subject = subject,
                Body = message,
                MessageCategory = category,
                SentAt = DateTime.Now,
                IsNew = true,
                IsDeleted = false,
                IsRecived = false,
                Sender = sender,
                SenderId = sender.Id,
                Recipient = recipient,
                RecipientId = recipient.Id
            };
        }

        public void MarckAsRead()
        {
            this.IsNew = false;
        }

        public void Delete()
        {
            this.IsDeleted = true;
        }

        public void MarckAsRecived()
        {
            this.IsRecived = true;
            this.RecivedAt = DateTime.Now;
        }

        public Message Replay(string message)
        {
            return new Message()
            {
                Subject = $"RE: {this.Subject}",
                Body = $"{message} {Environment.NewLine} {Environment.NewLine} {this.Body}",
                MessageCategory = this.MessageCategory,
                SentAt = DateTime.Now,
                IsNew = true,
                IsDeleted = false,
                IsRecived = false,
                Sender = this.Recipient,
                SenderId = this.RecipientId,
                Recipient = this.Sender,
                RecipientId = this.SenderId
            };
        }

        public Message Forward(ApplicationUser Recipient, string message = null)
        {
            return new Message()
            {
                Subject = $"FW: {this.Subject}",
                Body = $"{message} {Environment.NewLine} {Environment.NewLine} {this.Body}",
                MessageCategory = this.MessageCategory,
                SentAt = DateTime.Now,
                IsNew = true,
                IsDeleted = false,
                IsRecived = false,
                Sender = this.Recipient,
                SenderId = this.RecipientId,
                Recipient = Recipient,
                RecipientId = Recipient.Id
            };
        }
    }
}
