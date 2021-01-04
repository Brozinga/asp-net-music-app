using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MusicApp.Domain.Enums;
using MusicApp.Domain.Models;
using MusicApp.Infrastructure.Contexts;

namespace MusicApp.Shared.DI
{
    public static class IdentityInjection
    {
        public static void ConfigureEngine(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<SqliteContext>()
                .AddDefaultTokenProviders();
        }

        public static void ConfigurePassword(IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });
        }

        public static void ConfigurePolicies(IServiceCollection services)
        {
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Criador", policy => policy.RequireRole(ERoles.admin.ToString()));
                opt.AddPolicy("Admin", policy => policy.RequireRole(ERoles.admin.ToString()));
            });
        }
    }
}