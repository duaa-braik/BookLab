using BookLab.Domain.Models;
using static BookLab.Domain.Constants.AuthConstants;

namespace BookLab.Application.Interfaces;

public interface ITokenGeneratorService
{
    string Generate(UserModel user, TokenType tokenType);
}