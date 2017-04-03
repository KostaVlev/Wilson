namespace Wilson.Companies.Core.Entities
{
    public class Settings : Entity
    {
        public bool IsDatabaseInstalled { get; set; }

        public string HomeCompanyId { get; set; }
    }
}
