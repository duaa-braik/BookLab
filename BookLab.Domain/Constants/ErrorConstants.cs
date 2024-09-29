namespace BookLab.Domain.Constants;

public class ErrorConstants
{
    public const string ROLE_NOT_FOUND = "ROLE_NOT_FOUND";

    public static readonly Dictionary<string, string> Errors = new() 
    {
        { "ROLE_NOT_FOUND", "Role does not exist" }
    };
}