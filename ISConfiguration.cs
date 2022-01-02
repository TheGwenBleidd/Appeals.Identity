using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace Appeals.Identity
{
    /// <summary>
    /// Identuty Server Configuration
    /// </summary>
    public class ISConfiguration
    {
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>()
            {
                new ApiScope("AppealsWebAPI", "Web API")
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>()
            {
                new ApiResource("AppealsWebAPI", "Web API", new []
                    {JwtClaimTypes.Name})
                {
                    Scopes = {"AppealsWebAPI"}
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>()
            {
                new Client()
                {
                    ClientId = "appeals-web-api",
                    ClientName = "Appeals Web",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris =
                    {
                        "https://.../signin-oidc"
                    },
                    AllowedCorsOrigins = 
                    {
                        "https://..."
                    },
                    PostLogoutRedirectUris = 
                    {
                        "https://.../signout-oidc"   
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "AppealsWebAPI"
                    },
                    AllowAccessTokensViaBrowser = true,
                }
            };
    }
}
