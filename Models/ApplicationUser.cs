using Microsoft.AspNetCore.Identity;

namespace WebApiTravel.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
