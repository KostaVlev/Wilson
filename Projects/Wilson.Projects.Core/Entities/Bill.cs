using System;
using System.Collections.Generic;

namespace Wilson.Projects.Core.Entities
{
    public class Bill : Entity
    {
        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string HtmlContent { get; set; }

        public bool IsApproved { get; set; }

        public bool IsAccepted { get; set; }

        public Guid ProjectId { get; set; }

        public Guid CreatedById { get; set; }

        public virtual Project Project { get; set; }

        public virtual Employee CreatedBy { get; set; }

        public virtual ICollection<StorehouseItemBill> StorehouseItems { get; set; } = new HashSet<StorehouseItemBill>();
    }
}
