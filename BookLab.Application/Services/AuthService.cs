using BookLab.API.Dtos;
using BookLab.Application.Interfaces;
using BookLab.Domain.Entities;
using BookLab.Domain.Interfaces;
using BookLab.Domain.Models;
using BookLab.Infrastructure.Repositories;
using Mapster;

namespace BookLab.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(
        IUserRepository userRepository, 
        IRoleRepository roleRepository, 
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<CreateUserResponseModel> CreateUserAsync(CreateUserRequest request)
    {
        var createUserModel = request.Adapt<CreateUserModel>();

        createUserModel.UserName = createUserModel.Email.Split('@')[0];

        createUserModel.Password = _passwordHasher.Hash(request.Password);

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var role = await _roleRepository.GetRoleByName(createUserModel.Role);

            User newUser = createUserEntity(createUserModel, role);

            _userRepository.CreateUser(newUser);

            await _unitOfWork.SaveChangesAsync();

            transaction.Commit();

            var createdUser = newUser.Adapt<CreateUserResponseModel>();

            createdUser.Role = role.Name;

            return createdUser;
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }

    }

    private static User createUserEntity(CreateUserModel createUserModel, GetRoleModel role)
    {
        return new User
        {
            Email = createUserModel.Email,
            UserName = createUserModel.UserName,
            Password = createUserModel.Password,
            RoleId = role.Id,
            CreatedAt = DateTime.Now,
        };
    }
}