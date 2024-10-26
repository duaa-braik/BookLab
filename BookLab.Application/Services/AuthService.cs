using BookLab.API.Dtos;
using BookLab.Application.Dtos;
using BookLab.Application.Factories;
using BookLab.Application.Factories.UserFactory;
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
        var userModel = request.Adapt<UserModel>();

        setUsernameAndPassword(request, userModel);

        using var transaction = _unitOfWork.BeginTransaction();

        try
        {
            await checkIfAccountAlreadyExists(userModel);

            var role = await getRoleOrThrowAsync(userModel);

            var newUser = await createUserAsync(role, userModel);

            userModel.UserId = newUser.Id;

            var (accessToken, refreshToken) = await _sessionService.CreateSessionAsync(userModel);

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

    private async Task checkIfAccountAlreadyExists(UserModel userModel)
    {
        string? email = await _userRepository.GetUserEmailAsync(userModel.Email);

        if (email != null)
        {
            var error = _errorFactory.Create(ErrorType.ACCOUNT_EXISTS);

            throw new ConflictException(error);
        }
    }

    private async Task<GetRoleModel> getRoleOrThrowAsync(UserModel userModel)
    {
        var role = await _roleRepository.GetRoleByName(userModel.Role);

        if (role == null)
        {
            var error = _errorFactory.Create(ErrorType.ROLE_NOT_FOUND);

            throw new NotFoundException(error);
        }

        return role;
    }

    private void setUsernameAndPassword(CreateUserRequest request, UserModel userModel)
    {
        userModel.UserName = userModel.Email.Split('@')[0];

        userModel.Password = _hashService.Hash(request.Password);
    }

    private async Task<User> createUserAsync(GetRoleModel role, UserModel userModel)
    {
        var newUser = UserFactory.CreateUserByRole(role, userModel);

        _userRepository.CreateUser(newUser);

        await _unitOfWork.SaveChangesAsync();

        return newUser;
    }
}