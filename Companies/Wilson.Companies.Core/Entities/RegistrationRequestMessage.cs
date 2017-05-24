using System;

namespace Wilson.Companies.Core.Entities
{
    public class RegistrationRequestMessage : Entity
    {
        protected RegistrationRequestMessage()
        {
        }

        public DateTime SendAt { get; private set; }

        public DateTime? ReceivedAt { get; private set; }

        public bool IsNew { get; private set; }

        public bool IsDeleted { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Address { get; private set; }

        public string PrivatePhone { get; private set; }

        public string RecivedById { get; private set; }

        public virtual ApplicationUser RecivedBy { get; private set; }

        public static RegistrationRequestMessage Create(string firstName, string lastName, string privitePhone, Address address)
        {
            return new RegistrationRequestMessage()
            {
                SendAt = DateTime.Now,
                IsNew = true,
                PrivatePhone = privitePhone,
                FirstName = firstName,
                LastName = lastName,
                Address = address
            };
        }

        public void MarckAsRecived()
        {
            this.ReceivedAt = DateTime.Now;
            this.IsNew = false;
        }

        public void Delete()
        {
            this.IsDeleted = true;
        }

        public void Recive(ApplicationUser user)
        {
            this.RecivedBy = user;
            this.RecivedById = user.Id;
            this.ReceivedAt = DateTime.Now;
            this.IsNew = false;
        }

        public Address GetAddress()
        {
            return (Address)this.Address;
        }
    }
}
