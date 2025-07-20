using BookLab.Application.Dtos.Error;

namespace BookLab.API.Constants;

public class AuthErrors
{
    public static ErrorDto Forbidden = new () { Code = "FORBIDDED_ACCESS", Message = "You don't have access to this resource" };

    public static ErrorDto Unauthorized = new() { Code = "UNAUTHORIZED", Message = "You're either not authorized or an access token is missing" };
}
