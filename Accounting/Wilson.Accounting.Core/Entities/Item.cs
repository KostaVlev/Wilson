using System;
using System.Collections.Generic;
using Wilson.Accounting.Core.Enumerations;

namespace Wilson.Accounting.Core.Entities
{
    public class Item : Entity, IEquatable<Item>
    {
        private Item()
        {
        }

        public string Name { get; private set; }

        public Мeasure Мeasure { get; private set; }

        public virtual ICollection<InvoiceItem> InvoiceItems { get; private set; }

        public static Item Create(string name, Мeasure measure)
        {
            return new Item() { Name = name, Мeasure = measure, InvoiceItems = new HashSet<InvoiceItem>() };
        }

        public bool Equals(Item other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Name == other.Name && this.Мeasure == other.Мeasure;
        }
    }
}
