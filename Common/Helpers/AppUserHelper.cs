using Microsoft.AspNetCore.Identity;

namespace Appeals.Identity.Common.Helpers
{
    public static class AppUserHelper
    {
        public static PasswordOptions GetPasswordOptions() => new PasswordOptions()
        {
            RequiredLength = 8,
            RequiredUniqueChars = 1,
            RequireDigit = true,
            RequireLowercase = true,
            RequireNonAlphanumeric = true,
            RequireUppercase = true
        };
    }
}
