using BookLab.API.Dtos;

namespace BookLab.Application.Interfaces;

public interface IAuthService
{
    Task CreateUserAsync(CreateUserRequest request);
}
