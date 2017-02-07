using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace RamQuest.Security.Data.IdentityServer.Config
{
    public class ClientStore : IClientStore
    {
        public static IEnumerable<Client> Clients { get; } = new[]
        {
            new Client
            {
                //AccessTokenType = AccessTokenType.Reference,
                ClientId = "Tetris",
                ClientName = "Tetris Web Api",
                AccessTokenLifetime = 60*60*24,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                RequireClientSecret = false,
                AllowedScopes = {"openid", "TetrisApi", "TetrisIdentity"}
            }
        };

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            return Task.FromResult(Clients.FirstOrDefault(c => c.ClientId == clientId));
        }
    }
}