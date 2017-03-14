namespace Wilson.Scheduler.Core.Entities
{
    public class Project : Entity, IValueObject<Project>
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public bool Equals(Project other)
        {
            if (this.Name.Equals(other.Name))
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
