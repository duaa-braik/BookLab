﻿using BookLab.API.Dtos;
using BookLab.Application.Interfaces;
using BookLab.Domain.Constants;
using BookLab.Domain.Entities;
using BookLab.Domain.Exceptions;
using BookLab.Domain.Interfaces;
using BookLab.Domain.Models;
using BookLab.Infrastructure.Repositories;
using Mapster;
using static BookLab.Domain.Constants.ErrorConstants;
using static BookLab.Domain.Constants.AuthConstants;

namespace BookLab.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenGeneratorService _tokenGeneratorService;

    public AuthService(
        IUserRepository userRepository, 
        IRoleRepository roleRepository, 
        IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork,
        ITokenGeneratorService tokenGeneratorService)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _tokenGeneratorService = tokenGeneratorService;
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

            var accessToken = _tokenGeneratorService.Generate(createUserModel, TokenType.ACCESS_TOKEN);

            var createdUser = newUser.Adapt<CreateUserResponseModel>();

            createdUser.Role = role.Name;
            createdUser.AccessToken = accessToken;

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
            var errorCode = ErrorType.ROLE_NOT_FOUND;

            var error = new ErrorModel { Message = Errors[errorCode], ErrorCode = errorCode.ToString() };

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