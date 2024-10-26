
using BookLab.Application.Dtos;
using BookLab.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace BookLab.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    private const string ERROR_RESPONSE_TYPE = "application/json";

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (NotFoundException ex)
        {
            string message = ex.Message;
            string errorCode = ex.ErrorCode;
            int statusCode = (int)HttpStatusCode.NotFound;

            await handleException(httpContext, message, errorCode, statusCode);
        }
        catch (ConflictException ex)
        {
            string message = ex.Message;
            string errorCode = ex.ErrorCode;
            int statusCode = (int)HttpStatusCode.Conflict;

            await handleException(httpContext, message, errorCode, statusCode);
        }
    }

    private static async Task handleException(HttpContext httpContext, string message, string errorCode, int statusCode)
    {
        httpContext.Response.ContentType = ERROR_RESPONSE_TYPE;
        httpContext.Response.StatusCode = statusCode;

        var error = new ErrorDto
        {
            Message = message,
            Code = errorCode,
        };

        var response = new ErrorResponseDto
        {
            Errors = [error]
        };

        await httpContext.Response.WriteAsJsonAsync(response);
    }
}
