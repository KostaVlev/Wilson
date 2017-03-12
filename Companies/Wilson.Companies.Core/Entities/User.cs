using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Wilson.Companies.Core.Entities
{
    public class User : IdentityUser, IEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
