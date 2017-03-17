using System.Collections.Generic;

namespace Wilson.Projects.Core.Entities
{
    public class Employee : Entity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();

        public virtual ICollection<Bill> Bills { get; set; } = new HashSet<Bill>();
    }
}
