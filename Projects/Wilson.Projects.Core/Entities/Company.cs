namespace Wilson.Projects.Core.Entities
{
    public class Company : Entity
    {
        public string Name { get; private set; }

        public string GetName()
        {
            return this.Name;
        }
    }
}
