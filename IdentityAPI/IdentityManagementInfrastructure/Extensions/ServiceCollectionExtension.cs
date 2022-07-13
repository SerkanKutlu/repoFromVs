using IdentityManagementInfrastructure.Persistence;
using IdentityManagementInfrastructure.Services;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityManagementInfrastructure.Extensions;

public static class ServiceCollectionExtension
{

    public static IServiceCollection AddIdentityServerConfig(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 0;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = false;
            options.User.AllowedUserNameCharacters = "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+'#!/^%{}*";
        }).AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();

        services.AddIdentityServer()
            .AddDeveloperSigningCredential()
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder => builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.EnableTokenCleanup = true;
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder => builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            })
            .AddAspNetIdentity<AppUser>();
        return services;
    }

    public static IServiceCollection AddService<TUser>(this IServiceCollection services)where TUser: IdentityUser<int>
    {
        services.AddTransient<IProfileService, IdentityClaimsProfileServices>();
        return services;
    }
    public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppIdentityDbContext>(options=> options.UseSqlServer(connectionString));
        services.AddDbContext<AppPersistedGrantDbContext>(options=> options.UseSqlServer(connectionString));
        services.AddDbContext<AppConfigurationDbContext>(options=> options.UseSqlServer(connectionString));
        return services;
    }
    
}