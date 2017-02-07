using System.Linq;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RamQuest.Security.Data.IdentityServer;
using RamQuest.Security.Data.IdentityServer.Config;

namespace RamQuest.Security
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SeedIdentityServerConfigurationDb();
        }

        private static void SeedIdentityServerConfigurationDb()
        {
            var factory = new ConfigurationDbContextFactory();
            var context = factory.Create(new DbContextFactoryOptions
            {
                EnvironmentName = "Dev"
            });

            if (!context.Clients.Any())
            {
                foreach (var client in ClientStore.Clients)
                    context.Clients.Add(client.ToEntity());
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in ResourceStore.GetIdentityResources())
                    context.IdentityResources.Add(resource.ToEntity());
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in ResourceStore.GetApiResources())
                    context.ApiResources.Add(resource.ToEntity());
                context.SaveChanges();
            }
        }
    }
}