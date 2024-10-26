using BookLab.API.Dtos;
using BookLab.Domain.Models;

namespace BookLab.Application.Interfaces;

public interface IAuthService
{
    Task<CreateUserResponseModel> CreateUserAsync(CreateUserRequest request);
}
