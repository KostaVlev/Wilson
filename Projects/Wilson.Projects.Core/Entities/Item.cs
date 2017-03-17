using System.Collections.Generic;
using Wilson.Projects.Core.Enumerations;

namespace Wilson.Projects.Core.Entities
{
    public class Item : Entity, IValueObject<Item>
    {
        public string Name { get; set; }

        public Мeasure Мeasure { get; set; }
        
        public virtual ICollection<StorehouseItem> Storehouses { get; set; } = new HashSet<StorehouseItem>();

        public bool Equals(Item other)
        {
            if (this.Name.Equals(other.Name) && this.Мeasure == other.Мeasure)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
