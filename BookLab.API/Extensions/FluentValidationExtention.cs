using BookLab.Application.Validators.User;
using FluentValidation.AspNetCore;
using FluentValidation;

namespace BookLab.API.Extensions
{
    public static class FluentValidationExtention
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();

            return services;
        }
    }
}
