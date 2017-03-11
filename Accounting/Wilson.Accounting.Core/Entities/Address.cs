using System;
using System.Reflection;

namespace Wilson.Accounting.Core.Entities
{
    public class Address : Entity, IValueObject<Address>
    {
        public string Country { get; set; }

        public string PostCode { get; set; }

        public string Town { get; set; }

        public string Street { get; set; }

        public int StreetNumber { get; set; }

        public int? Floor { get; set; }

        public string UnitNumber { get; set; }

        public string Note { get; set; }

        public bool Equals(Address other)
        {
            if (this.Country.Equals(other.Country) && this.PostCode.Equals(other.PostCode) &&
                this.Town.Equals(other.Town) && this.Street.Equals(other.Street) &&
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
    }
}
