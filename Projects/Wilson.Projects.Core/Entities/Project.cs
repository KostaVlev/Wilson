using System;
using System.Collections.Generic;

namespace Wilson.Projects.Core.Entities
{
    public class Project : Entity
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime ActualEndDate { get; set; }

        public int GuaranteePeriodInMonths { get; set; }        

        public bool InProgress { get; set; }

        public string ManagerId { get; set; }

        public string StorehouseId { get; set; }

        public virtual Employee Manager { get; set; }

        public virtual Storehouse Storehouse { get; set; }

        public virtual ICollection<Bill> Bills { get; set; } = new HashSet<Bill>();
    }
}
