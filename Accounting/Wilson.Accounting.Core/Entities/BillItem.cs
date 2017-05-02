using System;

namespace Wilson.Accounting.Core.Entities
{
    public class BillItem : IValueObject<BillItem>
    {
        public int Quantity { get; private set; }

        public decimal Price { get; private set; }

        public string StorehouseItemId { get; set; }

        public string StorehouseId { get; private set; }

        public virtual StorehouseItem StorehouseItem { get; private set; }

        public virtual Storehouse Storehouse { get; private set; }

        public static BillItem Create(int quantity, decimal price, StorehouseItem storehouseItem)
        {
            if (quantity > storehouseItem.Quantity || quantity < 0)
            {
                throw new ArgumentOutOfRangeException("quantity", "Quantity can't be more then the available quantity and less then zero.");
            }

            if (price <= 0)
            {
                throw new ArgumentOutOfRangeException("price", "Price can't be negative number or zero.");
            }

            return new BillItem() { Quantity = quantity, StorehouseItem = storehouseItem, StorehouseItemId = storehouseItem.Id };
        }

        public void AddQuantity(int quantity)
        {
            this.Quantity += quantity;
        }

        public bool Equals(BillItem other)
        {
            if (other == null)
            {
                return false;
            }

            return this.StorehouseItem.Equals(other);
        }
    }
}
