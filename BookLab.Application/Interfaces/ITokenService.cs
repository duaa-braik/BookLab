using BookLab.Domain.Models;
using static BookLab.Domain.Constants.AuthConstants;

namespace BookLab.Application.Interfaces;

public interface ITokenService
{
    string Generate(UserModel user, TokenType tokenType);
}