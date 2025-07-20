using BookLab.API.Middlewares;
using BookLab.Application.Configurations;
using BookLab.Application.Factories;
using BookLab.Application.Interfaces;
using BookLab.Application.Services;
using BookLab.Application.Utils;
using BookLab.Domain.Interfaces;
using BookLab.Domain.Repositories;
using BookLab.Infrastructure.Repositories;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
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
            services.AddScoped<IHashService, HashService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IErrorFactory, ErrorFactory>();
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IBooksRepository, BooksRepository>();

            var config = new TypeAdapterConfig();

            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
            services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationMiddlewareResultHandler>();

            return services;
        }
    }
}
