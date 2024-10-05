using BookLab.Domain.Models;

namespace BookLab.Application.Interfaces;

public interface ITokenGeneratorService
{
    string Generate(CreateUserModel user);
}