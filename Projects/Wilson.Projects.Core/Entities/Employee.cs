using System.Collections.Generic;

namespace Wilson.Projects.Core.Entities
{
    public class Employee : Entity
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public virtual ICollection<Project> Projects { get; private set; }
    }
}
