using BookLab.Application.Interfaces;
using BookLab.Application.Services;
using BookLab.Domain.Interfaces;
using BookLab.Infrastructure.Repositories;

namespace BookLab.API.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
