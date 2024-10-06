namespace BookLab.Domain.Constants;

public class ErrorConstants
{
    public enum ErrorType
    {
        ROLE_NOT_FOUND
    }

    public static readonly Dictionary<ErrorType, string> Errors = new() 
    {
        { ErrorType.ROLE_NOT_FOUND, "Role does not exist" }
    };
}