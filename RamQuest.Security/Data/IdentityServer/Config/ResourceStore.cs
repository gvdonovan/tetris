using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace RamQuest.Security.Data.IdentityServer.Config
{
    public class ResourceStore : IResourceStore
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("TetrisApi", "Tetris Web API", new[] { JwtClaimTypes.Name, JwtClaimTypes.Role, "module" })
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
                            JwtClaimTypes.Email,
                            "module",
                            "module.permissions"
                        }
                }
            };
        }

        #region IResourceStore
        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<Resources> GetAllResources()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}