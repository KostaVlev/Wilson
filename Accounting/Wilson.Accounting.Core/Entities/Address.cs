using Newtonsoft.Json;
using System;

namespace Wilson.Accounting.Core.Entities
{
    [JsonObject]
    public class Address : ValueObject<Address>
    {
        protected Address()
        {
        }

        [JsonProperty]
        public string Country { get; private set; }

        [JsonProperty]
        public string PostCode { get; private set; }

        [JsonProperty]
        public string City { get; private set; }

        [JsonProperty]
        public string Street { get; private set; }

        [JsonProperty]
        public string StreetNumber { get; private set; }

        [JsonProperty]
        public int? Floor { get; private set; }

        [JsonProperty]
        public string UnitNumber { get; private set; }

        [JsonProperty]
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

        protected override bool EqualsCore(Address other)
        {
            if (this.Country.Equals(other.Country) && this.PostCode.Equals(other.PostCode) &&
                this.City.Equals(other.City) && this.Street.Equals(other.Street) &&
                this.StreetNumber.Equals(other.StreetNumber) && this.UnitNumber.Equals(other.UnitNumber))
            {
                return this.Floor != null && this.Floor == other.Floor ? true :  false;
            }
            else
            {
                return false;
            }
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = Country.GetHashCode();
                hashCode = (hashCode * 397) ^ PostCode.GetHashCode();
                hashCode = (hashCode * 397) ^ City.GetHashCode();
                hashCode = (hashCode * 397) ^ Street.GetHashCode();
                hashCode = (hashCode * 397) ^ PostCode.GetHashCode();
                hashCode = (hashCode * 397) ^ StreetNumber.GetHashCode();

                return hashCode;
            }
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
