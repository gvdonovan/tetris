﻿using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RamQuest.Security.Config;
using RamQuest.Security.Data;
using RamQuest.Security.Model;

namespace RamQuest.Security.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterEntityFramework(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(connectionString));
        }

        public static void RegisterIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ClaimsPrincipalFactory>();
        }

        public static void RegisterIdentityServer(this IServiceCollection services, string connectionString)
        {
            var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
            services.AddSingleton<IClientStore, ClientStore>();

            services.AddIdentityServer()
                .AddTemporarySigningCredential()

                .AddInMemoryApiResources(ResourceProvider.GetApiResources())                
                .AddInMemoryIdentityResources(ResourceProvider.GetIdentityResources())

                //.AddConfigurationStore(
                //    builder => builder.UseSqlServer(connectionString, options => options.MigrationsAssembly(migrationsAssembly)),
                //    options => options.DefaultSchema = "config")

                .AddOperationalStore(
                    builder => builder.UseSqlServer(connectionString, options => options.MigrationsAssembly(migrationsAssembly)),
                    options => options.DefaultSchema = "oper")

                .AddAspNetIdentity<ApplicationUser>();
            
        }

        public static void RegisterIdentityServices(this IServiceCollection services, string connectionString)
        {
            RegisterEntityFramework(services, connectionString);
            RegisterIdentity(services);
            RegisterIdentityServer(services, connectionString);
        }
    }
}