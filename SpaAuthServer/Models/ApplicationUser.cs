using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SpaAuthServer.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsAdmin { get; internal set; }
        public string SpaRole { get; set; }
    }
}
