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

    public async Task CreateUserAsync(CreateUserRequest request)
    {
        var createUserModel = request.Adapt<CreateUserModel>();

        createUserModel.UserName = createUserModel.Email.Split('@')[0];

        createUserModel.Password = _passwordHasher.Hash(request.Password);

        var roleId = await _roleRepository.GetRoleIdByName(createUserModel.Role);

        var newUser = new User
        {
            Email = createUserModel.Email,
            UserName = createUserModel.UserName,
            Password = createUserModel.Password,
            RoleId = roleId,
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