using IdentityServer4.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RamQuest.IdentityServer.Config;
using RamQuest.IdentityServer.Data;
using RamQuest.IdentityServer.Model;

namespace RamQuest.IdentityServer.Extensions
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

        public static void RegisterIdentityServer(this IServiceCollection services)
        {            
            services.AddSingleton<IClientStore, ClientStore>();

            services.AddIdentityServer()
                .AddTemporarySigningCredential()                
                .AddInMemoryApiResources(ResourceProvider.GetApiResources())                
                .AddInMemoryIdentityResources(ResourceProvider.GetIdentityResources())                
                .AddAspNetIdentity<ApplicationUser>();
        }

        public static void RegisterIdentityServer(this IServiceCollection services, string connectionString)
        {
            RegisterEntityFramework(services, connectionString);
            RegisterIdentity(services);
            RegisterIdentityServer(services);
        }
    }
}