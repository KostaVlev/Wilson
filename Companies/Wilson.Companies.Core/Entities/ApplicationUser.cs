using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Wilson.Companies.Core.Entities
{
    public class ApplicationUser : IdentityUser, IEntity
    {
        protected ApplicationUser()
        {
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public bool IsActive { get; private set; }

        public string EmployeeId { get; private set; }

        public virtual Employee Employee { get; private set; }

        public virtual ICollection<Message> SentMessages { get; private set; }

        public virtual ICollection<Message> ReceivedMessages { get; private set; }

        public virtual ICollection<RegistrationRequestMessage> RegistrationRequestMessages { get; private set; }

        public static ApplicationUser Create(Employee employee, string username)
        {
            return new ApplicationUser()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                UserName = username,
                Email = employee.Email,
                PhoneNumber = employee.Phone,
                EmployeeId = employee.Id,
                SentMessages = new HashSet<Message>(),
                ReceivedMessages = new HashSet<Message>(),
                RegistrationRequestMessages = new HashSet<RegistrationRequestMessage>()
            };
        }

        public static ApplicationUser CreateSystemUser(string firstName, string lastName, string email, string phone)
        {
            return new ApplicationUser()
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = email,
                Email = email,
                PhoneNumber = phone,
                SentMessages = new HashSet<Message>(),
                ReceivedMessages = new HashSet<Message>(),
                RegistrationRequestMessages = new HashSet<RegistrationRequestMessage>()
            };
        }

        public void UpdatePhone(string phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
        }

        public void ChangeNames(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string GetName()
        {
            return this.FirstName + " " + this.LastName;
        }

        public string GetName(Func<ApplicationUser, string> format)
        {
            return format(this);
        }

        public void Deactivate()
        {
            this.IsActive = false;
        }

        public void Activate()
        {
            this.IsActive = true;
        }
    }
}
