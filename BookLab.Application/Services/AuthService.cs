using BookLab.API.Dtos;
using BookLab.Application.Interfaces;
using BookLab.Domain.Entities;
using BookLab.Domain.Interfaces;
using BookLab.Domain.Models;
using Mapster;

namespace BookLab.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateUserAsync(CreateUserRequest request)
    {
        var createUserModel = request.Adapt<CreateUserModel>();

        createUserModel.UserName = createUserModel.Email.Split('@')[0];

        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Email = createUserModel.Email,
            UserName = createUserModel.UserName,
            Password = createUserModel.Password,
            CreatedAt = DateTime.Now,
        };

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            _userRepository.CreateUser(newUser);

            await _unitOfWork.SaveChangesAsync();

            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
        }

    }
}