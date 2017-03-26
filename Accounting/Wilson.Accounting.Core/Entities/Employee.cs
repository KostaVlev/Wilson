namespace Wilson.Accounting.Core.Entities
{
    public class Employee : Entity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}
