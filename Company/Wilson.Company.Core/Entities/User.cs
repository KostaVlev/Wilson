using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Wilson.Company.Core.Entities
{
    public class User : IdentityUser, IEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
