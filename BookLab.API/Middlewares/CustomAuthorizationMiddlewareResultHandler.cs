using BookLab.API.Constants;
using BookLab.Application.Dtos.Error;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using System.Text.Json;

namespace BookLab.API.Middlewares;

public class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
    private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
    {
        if (authorizeResult.Succeeded)
        {
            await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
            return;
        }

        var errorDetails = getErrorDetails(authorizeResult, context);

        context.Response.StatusCode = errorDetails.StatusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(JsonSerializer.Serialize(errorDetails.ErrorDto));
        return;
    }

    private ErrorDetails getErrorDetails(PolicyAuthorizationResult authorizeResult, HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true && authorizeResult.Forbidden)
            return new ErrorDetails { StatusCode = StatusCodes.Status403Forbidden, ErrorDto = AuthErrors.Forbidden };

        else return new ErrorDetails { StatusCode = StatusCodes.Status401Unauthorized, ErrorDto = AuthErrors.Unauthorized };
    }

    private class ErrorDetails
    {
        public int StatusCode { get; set; }
        public ErrorDto? ErrorDto { get; set; }
    }
}
