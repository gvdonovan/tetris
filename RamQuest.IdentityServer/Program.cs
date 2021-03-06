﻿using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RamQuest.Security.Config;
using RamQuest.Security.Data;
using System.Linq;

using IdentityServer4.EntityFramework.Mappers;

namespace RamQuest.Security
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var factory = new ConfigurationDbContextFactory();
            var context = factory.Create(new DbContextFactoryOptions
            {
               EnvironmentName = "Dev"
            });

            if (!context.Clients.Any())
            {
                foreach (var client in ClientStore.Clients)
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var resource in ResourceProvider.GetIdentityResources())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                foreach (var resource in ResourceProvider.GetApiResources())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
        }
    }
}
