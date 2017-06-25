using Newtonsoft.Json;
using System;

namespace Wilson.Accounting.Core.Entities.ValueObjects
{
    [JsonObject]
    public class BillItem : ValueObject<BillItem>
    {
        protected BillItem()
        {
        }

        [JsonProperty]
        public int Quantity { get; private set; }

        [JsonProperty]
        public decimal Price { get; private set; }

        [JsonProperty]
        public string StorehouseItemId { get; private set; }
        
        public virtual StorehouseItem StorehouseItem { get; private set; }

        public static BillItem Create(int quantity, decimal price, string storehouseItemId, int storehouseItemQuantity)
        {
            if (quantity > storehouseItemQuantity || quantity < 0)
            {
                throw new ArgumentOutOfRangeException("quantity", "Quantity can't be more then the available quantity and less then zero.");
            }

            if (price <= 0)
            {
                throw new ArgumentOutOfRangeException("price", "Price can't be negative number or zero.");
            }

            return new BillItem() { Quantity = quantity, StorehouseItemId = storehouseItemId };
        }

        public void AddQuantity(int quantity)
        {
            this.Quantity += quantity;
        }

        protected override bool EqualsCore(BillItem other)
        {
            if (other == null)
            {
                return false;
            }

            return this.StorehouseItemId == other.StorehouseItemId;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = StorehouseItemId.GetHashCode();
                hashCode = (hashCode * 397) ^ Quantity;
                hashCode = (hashCode * 397) ^ Price.GetHashCode();

                return hashCode;
            }
        }
    }
}
