using BookLab.API.Dtos;
using BookLab.Application.Interfaces;
using BookLab.Domain.Constants;
using BookLab.Domain.Entities;
using BookLab.Domain.Exceptions;
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

        setUsernameAndPassword(request, createUserModel);

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var role = await getRoleOrThrow(createUserModel);

            User newUser = await createUser(createUserModel, role.Id);

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

    private async Task<GetRoleModel> getRoleOrThrow(CreateUserModel createUserModel)
    {
        var role = await _roleRepository.GetRoleByName(createUserModel.Role);

        if (role == null)
        {
            string errorCode = ErrorConstants.ROLE_NOT_FOUND;

            var error = new ErrorModel { Message = ErrorConstants.Errors[errorCode], ErrorCode = errorCode };

            throw new NotFoundException(error);
        }

        return role;
    }

    private void setUsernameAndPassword(CreateUserRequest request, CreateUserModel createUserModel)
    {
        createUserModel.UserName = createUserModel.Email.Split('@')[0];

        createUserModel.Password = _passwordHasher.Hash(request.Password);
    }

    private async Task<User> createUser(CreateUserModel createUserModel, int roleId)
    {
        var newUser = new User
        {
            Email = createUserModel.Email,
            UserName = createUserModel.UserName,
            Password = createUserModel.Password,
            RoleId = roleId,
            CreatedAt = DateTime.Now,
        };

        _userRepository.CreateUser(newUser);

        await _unitOfWork.SaveChangesAsync();

        return newUser;
    }
}