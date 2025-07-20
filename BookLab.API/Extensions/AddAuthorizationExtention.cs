using System.Security.Claims;

namespace BookLab.API.Extensions
{
    public static class AddAuthorizationExtention
    {
        public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorizationBuilder()
                .AddPolicy("Admin", policy => policy.RequireRole("Admin"));

            return services;
        }
    }
}
