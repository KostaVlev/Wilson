namespace Wilson.Accounting.Core.Entities
{
    public class Address : Entity
    {
        public string Country { get; set; }

        public string PostCode { get; set; }

        public string Town { get; set; }

        public string Street { get; set; }

        public int StreetNumber { get; set; }

        public int? Floor { get; set; }

        public string UnitNumber { get; set; }

        public string Note { get; set; }
    }
}
