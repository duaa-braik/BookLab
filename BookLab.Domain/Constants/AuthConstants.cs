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

    public enum ClaimType
    {
        UserId,
        Email,
        Role,
        Expiration
    }

    public static readonly Dictionary<TokenType, double> Expiration = new()
    {
        { TokenType.ACCESS_TOKEN, 1 },
        { TokenType.REFRESH_TOKEN, 7 }
    };

    public static readonly Dictionary<ClaimType, string> Claims = new()
    {
        { ClaimType.UserId, "userId" },
        { ClaimType.Email, "email" },
        { ClaimType.Role, "role" },
        { ClaimType.Expiration, "exp" }
    };
}