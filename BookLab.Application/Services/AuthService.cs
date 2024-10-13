﻿using BookLab.API.Dtos;
using BookLab.Application.Factories;
using BookLab.Application.Interfaces;
using BookLab.Domain.Entities;
using BookLab.Domain.Exceptions;
using BookLab.Domain.Interfaces;
using BookLab.Domain.Models;
using BookLab.Infrastructure.Repositories;
using Mapster;
using static BookLab.Domain.Constants.ErrorConstants;

namespace BookLab.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoleRepository _roleRepository;
    private readonly IHashService _hashService;
    private readonly ISessionService _sessionService;
    private readonly IErrorFactory _errorFactory;

    public AuthService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IHashService hashService,
        IUnitOfWork unitOfWork,
        ITokenGeneratorService tokenGeneratorService,
        ISessionService sessionService,
        IErrorFactory errorFactory)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
        _hashService = hashService;
        _sessionService = sessionService;
        _errorFactory = errorFactory;
    }

    public async Task<CreateUserResponseModel> CreateUserAsync(CreateUserRequest request)
    {
        var createUserModel = request.Adapt<CreateUserModel>();

        setUsernameAndPassword(request, createUserModel);

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            await checkIfAccountAlreadyExists(createUserModel);

            var role = await getRoleOrThrow(createUserModel);

            User newUser = await createUser(createUserModel, role.Id);

            createUserModel.UserId = newUser.Id;

            var (accessToken, refreshToken) = await _sessionService.CreateSessionAsync(createUserModel);

            var createdUser = newUser.Adapt<CreateUserResponseModel>();

            createdUser.Role = role.Name;
            createdUser.AccessToken = accessToken;
            createdUser.RefreshToken = refreshToken;

            transaction.Commit();

            return createdUser;
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    private async Task checkIfAccountAlreadyExists(CreateUserModel createUserModel)
    {
        string? email = await _userRepository.GetUserEmailAsync(createUserModel.Email);

        if (email != null)
        {
            var error = _errorFactory.Create(ErrorType.ACCOUNT_EXISTS);

            throw new ConflictException(error);
        }
    }

    private async Task<GetRoleModel> getRoleOrThrow(CreateUserModel createUserModel)
    {
        var role = await _roleRepository.GetRoleByName(createUserModel.Role);

        if (role == null)
        {
            var error = _errorFactory.Create(ErrorType.ROLE_NOT_FOUND);

            throw new NotFoundException(error);
        }

        return role;
    }

    private void setUsernameAndPassword(CreateUserRequest request, CreateUserModel createUserModel)
    {
        createUserModel.UserName = createUserModel.Email.Split('@')[0];

        createUserModel.Password = _hashService.Hash(request.Password);
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