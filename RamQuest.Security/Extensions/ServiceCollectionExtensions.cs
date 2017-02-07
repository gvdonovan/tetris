using System.Reflection;
using IdentityServer4.EntityFramework.Stores;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RamQuest.Security.Data.Identity;
using RamQuest.Security.Model;

namespace RamQuest.Security.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterEntityFramework(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Data.Identity.IdentityDbContext>(options => options.UseSqlServer(connectionString));
        }

        public static void RegisterIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<Data.Identity.IdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ClaimsPrincipalFactory>();
        }

        public static void RegisterIdentityServer(this IServiceCollection services, string connectionString)
        {
            var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
            services.AddSingleton<IClientStore, Data.IdentityServer.Config.ClientStore>();

            services.AddIdentityServer()
                .AddTemporarySigningCredential()

                .AddInMemoryApiResources(Data.IdentityServer.Config.ResourceStore.GetApiResources())
                .AddInMemoryIdentityResources(Data.IdentityServer.Config.ResourceStore.GetIdentityResources())

                //.AddConfigurationStore(
                //    builder => builder.UseSqlServer(connectionString, options => options.MigrationsAssembly(migrationsAssembly)),
                //    options => options.DefaultSchema = "config")

                .AddOperationalStore(
                    builder => builder.UseSqlServer(connectionString, options => options.MigrationsAssembly(migrationsAssembly)),
                    options => options.DefaultSchema = "oper")

                .AddAspNetIdentity<ApplicationUser>();
        }

        public static void RegisterSecurityServices(this IServiceCollection services, string connectionString)
        {
            RegisterEntityFramework(services, connectionString);
            RegisterIdentity(services);
            RegisterIdentityServer(services, connectionString);
        }
    }
}
