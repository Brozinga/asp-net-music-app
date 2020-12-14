using Microsoft.Extensions.DependencyInjection;
using MusicApp.Domain.Interfaces.Repositories;
using MusicApp.Infrastructure.Repositories;
using MusicApp.Services.Handlers;

namespace MusicApp.Shared.DI
{
    public static class ClassInjection
    {
        public static void ConfigureRepository(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IMusicRepository, MusicRepository>();
        }

        public static void ConfigureHandle(IServiceCollection services)
        {
            services.AddScoped<UserHandle>();
        }
    }
}