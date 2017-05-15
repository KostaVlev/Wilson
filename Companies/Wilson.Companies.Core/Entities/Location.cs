using Newtonsoft.Json;

namespace Wilson.Companies.Core.Entities
{
    public class Location : IValueObject<Location>
    {
        protected Location()
        {
        }

        public string Country { get; private set; }

        public string PostCode { get; private set; }

        public string City { get; private set; }

        public string Street { get; private set; }

        public string Note { get; private set; }

        public string StreetNumber { get; private set; }

        public static Location Create(string country, string postCode, string city, string street, string note, string streetNumber)
        {
            return new Location()
            {
                Country = country,
                PostCode = postCode,
                City = city,
                Street = street,
                Note = note,
                StreetNumber = streetNumber
            };
        }

        public bool Equals(Location other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Country == other.Country &&
                   this.PostCode == other.PostCode &&
                   this.City == other.City &&
                   this.Street == other.Street &&
                   this.StreetNumber == other.StreetNumber;
        }

        public static explicit operator Location(string address)
        {
            var result = JsonConvert.DeserializeObject<Address>(address);

            return Create(
                result.Country,
                result.PostCode,
                result.City,
                result.Street,                
                result.Note,
                result.StreetNumber);
        }

        public static implicit operator string(Location address)
        {
            return JsonConvert.SerializeObject(address);
        }
    }
}
