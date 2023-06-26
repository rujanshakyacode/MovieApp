using Microsoft.AspNetCore.Identity;

namespace MovieApp.Data
{
    public class ApplicationUser:IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        
    }
}
