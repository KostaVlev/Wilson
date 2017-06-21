using Wilson.Accounting.Core.Entities;
using Xunit;

namespace Wilson.Accounting.Tests.Common
{
    public class AddressTest
    {
        private readonly Address address;

        public AddressTest()
        {
            this.address = this.GetAddress();
        }

        [Fact]
        public void Create_Valid_Address()
        {
            var country = "Bulgaria";
            var postCode = "4000";
            var city = "Plovdiv";
            var street = "Porto Lagos";
            var streetNumer = "2A";

            var address = this.address;
            var shortAddress = Address.Create(country, postCode, city, street, streetNumer);

            Assert.NotNull(address);
            Assert.NotNull(shortAddress);
        }

        [Fact]
        public void Check_If_Two_Addresses_Are_The_Same()
        {
            var country = "Bulgaria";
            var diffrentCountry = "Italy";
            var postCode = "4000";
            var city = "Plovdiv";
            var street = "Porto Lagos";
            var streetNumer = "2A";
            var floor = 5;
            var unitNumber = "3";
            var note = "Simple note";

            var addressA = Address.Create(country, postCode, city, street, streetNumer, floor, unitNumber, note);
            var addressB = Address.Create(country, postCode, city, street, streetNumer);
            var addressC = Address.Create(diffrentCountry, postCode, city, street, streetNumer, floor, unitNumber, note);
            var addressD = this.address;

            Assert.True(addressA == addressD);
            Assert.False(addressA == addressB);
            Assert.False(addressA == addressC);
            Assert.True(addressA.EqualsCore(addressD));
            Assert.False(addressA.EqualsCore(addressB));
            Assert.False(addressA.EqualsCore(addressC));
        }

        private Address GetAddress()
        {
            var country = "Bulgaria";
            var postCode = "4000";
            var city = "Plovdiv";
            var street = "Porto Lagos";
            var streetNumer = "2A";
            var floor = 5;
            var unitNumber = "3";
            var note = "Simple note";

            return Address.Create(country, postCode, city, street, streetNumer, floor, unitNumber, note);
        }
    }
}
