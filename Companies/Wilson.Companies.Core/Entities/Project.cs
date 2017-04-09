namespace Wilson.Companies.Core.Entities
{
    public class Project : Entity
    {
        public string Name { get; set; }

        public bool IsActive { get; set; }

        public string LocationId { get; set; }

        public string CustomerId { get; set; }

        public string ContractId { get; set; }

        public virtual ProjectLocation Location { get; set; }

        public virtual Company Customer { get; set; }

        public virtual CompanyContract Contract { get; set; }
    }
}
