using Microsoft.AspNetCore.Identity;

namespace ThuPointOfSaleFinal.App.Models
{
    public class AppUser : IdentityUser
    {
        public string? Address { get; set; }
    }
}
