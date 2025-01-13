using BookLab.Domain.Models.User;
using static BookLab.Domain.Constants.AuthConstants;

namespace BookLab.Application.Interfaces;

public interface ITokenService
{
    string Generate(UserModel user, TokenType tokenType);
    string GetClaimFromJwtToken(string token, ClaimType type);
    bool Validate(string token);
}