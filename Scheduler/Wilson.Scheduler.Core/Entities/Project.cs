namespace Wilson.Scheduler.Core.Entities
{
    public class Project : Entity, IValueObject<Project>
    {
        public string Name { get; private set; }

        public string ShortName { get; private set; }

        public bool IsActive { get; private set; }

        public static Project Create(string name)
        {
            return new Project()
            {
                Name = name,
                ShortName = name.Substring(0, 4).ToUpper(),
                IsActive = true
            };
        }

        public void Activate()
        {
            this.IsActive = true;
        }

        public void Deactivate()
        {
            this.IsActive = false;
        }

        public bool Equals(Project other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Name.Equals(other.Name);
        }
    }
}
