using Wilson.Companies.Core.Interfaces;

namespace Wilson.Companies.Core.Entities
{
    public class Settings : Entity, ISettings
    {
        public bool IsDatabaseInstalled { get; set; }

        public string HomeCompanyId { get; set; }
    }
}
