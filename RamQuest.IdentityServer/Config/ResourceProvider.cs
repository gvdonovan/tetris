using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Models;

namespace RamQuest.IdentityServer.Config
{
    public class ResourceProvider
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("TetrisApi", "Tetris Web API",
                    new[] {JwtClaimTypes.Name, JwtClaimTypes.Role, "module"})
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "TetrisIdentity",
                    UserClaims =
                        new[]
                        {
                            JwtClaimTypes.Name,
                            JwtClaimTypes.Role,
                            JwtClaimTypes.GivenName,
                            JwtClaimTypes.FamilyName,
                            "module",
                            "module.permissions"
                        }
                }
            };
        }
    }
}