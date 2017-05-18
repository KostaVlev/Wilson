using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Wilson.Companies.Core.Entities
{
    public class ApplicationRole : IdentityRole
    {
        protected ApplicationRole()
        {
        }
        
        public string Description { get; private set; }

        public static ApplicationRole Create(string roleName, string description)
        {
            return new ApplicationRole()
            {
                Description = description,
                Name = roleName
            };
        }
    }
}
