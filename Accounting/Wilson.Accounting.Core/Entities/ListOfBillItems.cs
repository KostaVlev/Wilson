using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Wilson.Accounting.Core.Entities
{
    [JsonObject]
    public class  ListOfBillItems : ValueObject<ListOfBillItems>, IEnumerable<BillItem>
    {
        [JsonProperty]
        private IList<BillItem> BillItems { get; set; }

        protected ListOfBillItems()
        {
        }

        public static ListOfBillItems Create()
        {
            return new ListOfBillItems() { BillItems = new List<BillItem>() };
        }

        public static ListOfBillItems Create(IEnumerable<BillItem> billItem)
        {
            return new ListOfBillItems() { BillItems = billItem.ToList() };
        }

        public decimal Sum()
        {
            return this.BillItems.Sum(x => (x.Quantity * x.StorehouseItem.Price));
        }

        public bool Contains(BillItem billItem)
        {
            return this.BillItems.Contains(billItem);
        }

        public ListOfBillItems Add(BillItem billItem)
        {
            this.BillItems.Add(billItem);
            return ListOfBillItems.Create(this.BillItems);
        }

        public ListOfBillItems AddRange(ListOfBillItems billItems)
        {
            this.BillItems.ToList().AddRange(billItems);
            return ListOfBillItems.Create(this.BillItems);
        }

        public bool Equals(ListOfBillItems other)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<BillItem> GetEnumerator()
        {
            return this.BillItems.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        protected override bool EqualsCore(ListOfBillItems other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = this.BillItems.GetHashCode();
                return hashCode;
            }
        }

        public static explicit operator ListOfBillItems(string billItemsList)
        {
            var billItems = JsonConvert.DeserializeObject<IEnumerable<BillItem>>(billItemsList);

            return Create(billItems);
        }

        public static implicit operator string(ListOfBillItems listOfBillItems)
        {
            return JsonConvert.SerializeObject(listOfBillItems.BillItems);
        }
    }
}
