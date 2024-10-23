namespace BookLab.Domain.Constants;

public class AuthConstants
{
    public enum TokenType
    {
        ACCESS_TOKEN,
        REFRESH_TOKEN
    }

    public enum RoleType
    {
        Customer,
        Admin,
    }

    public static readonly Dictionary<TokenType, double> Expiration = new()
    {
        { TokenType.ACCESS_TOKEN, 1 },
        { TokenType.REFRESH_TOKEN, 7 }
    };
}