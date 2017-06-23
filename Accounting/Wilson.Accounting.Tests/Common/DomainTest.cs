using System;
using System.Collections.Generic;
using System.Linq;
using Wilson.Accounting.Core.Entities;
using Xunit;

namespace Wilson.Accounting.Tests.Common
{
    public class DomainTest
    {
        private readonly Address address;

        public DomainTest()
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
            Assert.True(addressA.Equals(addressD));
            Assert.False(addressA.Equals(addressB));
            Assert.False(addressA.Equals(addressC));
        }

        [Fact]
        public void Create_Valid_Bill()
        {
            var date = new DateTime(2017, 6, 22);
            var projectId = Guid.NewGuid().ToString();
            var billItems = ListOfBillItems.Create();

            var bill = Bill.Create(date, billItems, projectId);

            Assert.NotNull(bill);
            Assert.NotNull(bill.BillItems);
            Assert.True(bill.Date.Equals(date));
            Assert.True(bill.ProjectId.Equals(projectId));
            Assert.False(bill.HasInvoice);
        }

        [Fact]
        public void Attache_Invoice_to_Bill()
        {
            var date = new DateTime(2017, 6, 19);
            var billItems = ListOfBillItems.Create();
            var projectId = Guid.NewGuid().ToString();
            var invoiceId = Guid.NewGuid().ToString();
            var otherInviceId = Guid.NewGuid().ToString();

            var bill = Bill.Create(date, billItems, projectId);
            bill.AttachInvoice(invoiceId);

            Assert.NotNull(bill.InvoiceId);
            Assert.True(bill.HasInvoice);

            Assert.Throws<InvalidOperationException>(() => bill.AttachInvoice(otherInviceId));
        }

        [Fact]
        public void Revise_Bill()
        {
            var date = new DateTime(2017, 6, 19);
            var revisionDate = new DateTime(2017, 6, 22);
            var billItems = ListOfBillItems.Create();
            var projectId = Guid.NewGuid().ToString();
            var storeHouseItemId = Guid.NewGuid().ToString();
            var storehuseItemQuantity = 6;
            var billItem = BillItem.Create(5, 2.5M, storeHouseItemId, storehuseItemQuantity);
            var revisedBillItem = BillItem.Create(4, 2.5M, storeHouseItemId, storehuseItemQuantity);
            var items = new List<BillItem> { billItem };
            var revisedItems = new List<BillItem> { revisedBillItem };

            var bill = Bill.Create(date, billItems, projectId);
            bill.Revise(revisionDate, ListOfBillItems.Create(revisedItems));

            Assert.NotNull(bill);
            Assert.NotNull(bill.BillItems);
            Assert.True(bill.GetBillItems().Count() == 1);
            Assert.True(bill.GetBillItems().First().Quantity == revisedBillItem.Quantity);
            Assert.True(bill.RevisionDate.Equals(revisionDate));
                       
            var revisonDatePriorTheDate = date.AddDays(-5);
            Assert.Throws<ArgumentOutOfRangeException>(() => bill.Revise(revisonDatePriorTheDate, ListOfBillItems.Create(revisedItems)));

            var revisonDatePriorTheLastRevisionDate = bill.RevisionDate.AddDays(-1);
            Assert.Throws<ArgumentOutOfRangeException>(() => bill.Revise(revisonDatePriorTheLastRevisionDate, ListOfBillItems.Create(revisedItems)));

            bill.AttachInvoice(Guid.NewGuid().ToString());
            Assert.Throws<InvalidOperationException>(() => bill.Revise(revisionDate, ListOfBillItems.Create(revisedItems)));
        }

        [Fact]
        public void Adding_BillItem_To_BIll()
        {
            var date = new DateTime(2017, 6, 19);
            var billItems = ListOfBillItems.Create();
            var projectId = Guid.NewGuid().ToString();

            var storeHouseItemAId = Guid.NewGuid().ToString();
            var storehuseItemAQuantity = 15;
            var billItemA = BillItem.Create(5, 2.5M, storeHouseItemAId, storehuseItemAQuantity);

            var storeHouseItemBId = Guid.NewGuid().ToString();
            var storehuseItemBQuantity = 15;
            var billItemB = BillItem.Create(4, 2.5M, storeHouseItemBId, storehuseItemBQuantity);

            var bill = Bill.Create(date, billItems, projectId);
            Assert.True(bill.GetBillItems().Count() == 0);

            bill.AddBillItem(billItemA);
            Assert.True(bill.GetBillItems().Count() == 1);

            bill.AddBillItem(billItemB);
            Assert.True(bill.GetBillItems().Count() == 2);

            bill.AddBillItem(billItemA);
            Assert.True(bill.GetBillItems().Count() == 2);
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
