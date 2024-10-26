using BookLab.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BookLab.API.Extensions
{
    public static class BadRequestResponseExtension
    {
        public static IServiceCollection ConfigureBadRequestResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                 options.InvalidModelStateResponseFactory = context =>
                 {
                     var errors = context.ModelState
                         .Where(e => e.Value?.Errors.Count > 0)
                         .Select(e => e.Value.Errors)
                         .SelectMany(e => e.Select(x => new ErrorDto { Message = x.ErrorMessage }))
                         .ToList();

                     var errorResponse = new ErrorResponseDto { Errors = errors };

                     return new BadRequestObjectResult(errorResponse);
                 };
            });

            return services;
        }
    }
}
