using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

namespace Wilson.Companies.Core.Entities
{
    public class User : IdentityUser, IEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public string EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ICollection<Message> SentMessages { get; set; } = new HashSet<Message>();

        public virtual ICollection<Message> ReceivedMessages { get; set; } = new HashSet<Message>();

        public virtual ICollection<RegistrationRequestMessage> RegistrationRequestMessages { get; set; } = new HashSet<RegistrationRequestMessage>();
    }
}
