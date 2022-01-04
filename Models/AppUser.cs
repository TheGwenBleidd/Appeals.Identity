using Microsoft.AspNetCore.Identity;

namespace Appeals.Identity.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser(RegisterViewModel model)
        {
            this.UserName = model.UserName;
            this.Email = model.UserName;
        }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
    }
}
