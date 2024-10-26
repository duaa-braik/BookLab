using BookLab.Application.Configurations;

namespace BookLab.API.Extensions;

public static class AppConfigurations
{
    public static IServiceCollection ConfigureAppSettings(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<JwtConfig>(configuration.GetSection(typeof(JwtConfig).Name));

        return services;
    }
}
