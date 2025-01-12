using BookLab.API.Dtos;
using BookLab.Application.Dtos;
using BookLab.Domain.Models;

namespace BookLab.Application.Interfaces;

public interface IAuthService
{
    Task<CreateUserResponseModel> CreateUserAsync(CreateUserRequest request);

    Task<LoginResponseModel> LoginUserAsync(LoginUserRequest request);
    Task LogoutUserAsync(string token);
}
