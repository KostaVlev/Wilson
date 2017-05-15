using Newtonsoft.Json;
using System;

namespace Wilson.Companies.Core.Entities
{
    public class Address : IValueObject<Address>
    {
        protected Address()
        {
        }

        public string Country { get; private set; }

        public string PostCode { get; private set; }

        public string City { get; private set; }

        public string Street { get; private set; }

        public string StreetNumber { get; private set; }

        public int? Floor { get; private set; }

        public string UnitNumber { get; private set; }

        public string Note { get; private set; }

        public static Address Create(
            string country,
            string postCode,
            string city,
            string street,
            string streetNumber,
            int? floor = null,
            string unitNumber = null,
            string note = null)
        {
            var address = new Address()
            {
                Country = country,
                PostCode = postCode,
                City = city,
                Street = street,
                StreetNumber = streetNumber,
                Floor = floor,
                UnitNumber = unitNumber,
                Note = note
            };

            Validate(address);

            return address;
        }

        public bool Equals(Address other)
        {
            if (this.Country.Equals(other.Country) && this.PostCode.Equals(other.PostCode) &&
                this.City.Equals(other.City) && this.Street.Equals(other.Street) &&
                this.StreetNumber.Equals(other.StreetNumber) && this.UnitNumber.Equals(other.UnitNumber))
            {
                if (this.Floor != null && this.Floor == other.Floor)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public static explicit operator Address(string address)
        {
            var result = JsonConvert.DeserializeObject<Address>(address);

            return Create(
                result.Country,
                result.PostCode,
                result.City,
                result.Street,
                result.StreetNumber,
                result.Floor,
                result.UnitNumber,
                result.Note);
        }

        public static implicit operator string(Address address)
        {
            return JsonConvert.SerializeObject(address);
        }

        private static void Validate(Address address)
        {
            if (string.IsNullOrEmpty(address.Country) && string.IsNullOrWhiteSpace(address.Country))
            {
                throw new ArgumentNullException("address.Country", "Country is required.");
            }

            if (string.IsNullOrEmpty(address.PostCode) && string.IsNullOrWhiteSpace(address.PostCode))
            {
                throw new ArgumentNullException("address.PostCode", "PostCode is required.");
            }

            if (string.IsNullOrEmpty(address.Street) && string.IsNullOrWhiteSpace(address.Street))
            {
                throw new ArgumentNullException("address.Street", "Street name is required.");
            }

            if (string.IsNullOrEmpty(address.StreetNumber) && string.IsNullOrWhiteSpace(address.StreetNumber))
            {
                throw new ArgumentNullException("address.StreetNumber", "Street Number is required.");
            }
        }
    }
}
