using System;
using System.Collections.Generic;
using Wilson.Companies.Core.Enumerations;

namespace Wilson.Companies.Core.Entities
{
    public class Employee : Entity
    {
        private Employee()
        {                
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Phone { get; private set; }

        public string PrivatePhone { get; private set; }

        public string Email { get; private set; }

        public string Address { get; private set; }

        public EmployeePosition EmployeePosition { get; private set; }

        public EmployeePosition? PositionBeforePromotion { get; private set; }

        public bool IsFired { get; private set; }

        public DateTime HiredAt { get; private set; }

        public DateTime? FiredAt { get; private set; }

        public DateTime? PromotionDate { get; private set; }

        public string CompanyId { get; private set; }

        public virtual Company Company { get; private set; }

        public ICollection<InfoRequest> InfoRequests { get; private set; }

        public static Employee Create(
            string firstName, 
            string lastName, 
            string phone, 
            string privatePhone, 
            string email, 
            EmployeePosition position,
            Address address, 
            string companyId)
        {
            return new Employee()
            {
                FirstName = firstName,
                LastName = lastName,
                Phone = phone,
                PrivatePhone = privatePhone,
                Email = email,
                EmployeePosition = position,
                Address = address,
                CompanyId = companyId,
                HiredAt = DateTime.Now                
            };
        }

        public void Fire()
        {
            this.IsFired = true;
            this.FiredAt = DateTime.Now;
        }

        public void Promote(EmployeePosition newPosition)
        {
            this.PositionBeforePromotion = this.EmployeePosition;
            this.EmployeePosition = newPosition;
            this.PromotionDate = DateTime.Now;
        }

        public void ChangePhone(string phone, bool isPrivitePhone = false)
        {
            if (!isPrivitePhone)
            {
                this.Phone = phone;
            }
            else
            {
                this.PrivatePhone = phone;
            }
        }

        public void ChangeEmail(string email)
        {
            this.Email = email;
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

        public string GetName(Func<Employee, string> format)
        {
            return format(this);
        }

        public string GetEmail()
        {
            return this.Email;
        }

        public string GetPhone()
        {
            return this.Phone;
        }

        public string GetPrivatePhone()
        {
            return this.PrivatePhone;
        }

        public Address GetAddress()
        {
            return (Address)this.Address;
        }
    }
}
