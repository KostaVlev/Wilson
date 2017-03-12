using System;

namespace Wilson.Companies.Core.Entities
{
    public class Project : Entity
    {
        public string Name { get; set; }

        public Guid LocationId { get; set; }

        public Guid CustomerId { get; set; }

        public Guid ContractId { get; set; }

        public virtual ProjectLocation Location { get; set; }

        public virtual Company Customer { get; set; }

        public virtual CompanyContract Contract { get; set; }
    }
}
