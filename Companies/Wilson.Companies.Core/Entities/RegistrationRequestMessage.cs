using System;

namespace Wilson.Companies.Core.Entities
{
    public class RegistrationRequestMessage : Address
    {
        public DateTime SendAt { get; set; }

        public DateTime? ReceivedAt { get; set; }

        public bool IsNew { get; set; }

        public bool IsDeleted { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PrivatePhone { get; set; }

        public string RecipientId { get; set; }

        public virtual User Recipient { get; set; }
    }
}
