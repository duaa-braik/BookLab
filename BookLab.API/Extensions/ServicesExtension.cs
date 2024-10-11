using BookLab.Application.Configurations;
using BookLab.Application.Interfaces;
using BookLab.Application.Services;
using BookLab.Application.Utils;
using BookLab.Domain.Interfaces;
using BookLab.Infrastructure.Repositories;
using Microsoft.Extensions.Options;

namespace BookLab.API.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<ISessionService, SessionService>();

            return services;
        }
    }
}
