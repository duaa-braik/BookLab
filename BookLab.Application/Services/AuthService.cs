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
using static BookLab.Domain.Constants.AuthConstants;
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
    private readonly ITokenService _tokenService;

    public AuthService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IHashService hashService,
        IUnitOfWork unitOfWork,
        ISessionService sessionService,
        IErrorFactory errorFactory,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
        _hashService = hashService;
        _sessionService = sessionService;
        _errorFactory = errorFactory;
        _tokenService = tokenService;
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

            await createUserAsync(role, userModel);

            await createSession(userModel);

            var createdUser = userModel.Adapt<CreateUserResponseModel>();

            transaction.Commit();

            return createdUser;
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task<LoginResponseModel> LoginUserAsync(LoginUserRequest request)
    {
        var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var userModel = await getUserOrThrowAsync(request.Email);

            verifyPassword(request.Password, userModel.Password);

            await createSession(userModel);

            var response = userModel.Adapt<LoginResponseModel>();

            transaction.Commit();

            return response;
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task LogoutUserAsync(string token)
    {
        var userId = _tokenService.GetClaimFromJwtToken(token, ClaimType.UserId);

        var transaction = _unitOfWork.BeginTransaction();

        try
        {
            var user = await _userRepository.GetUserByUserId(new Guid(userId));

            if(user == null)
            {
                var error = _errorFactory.Create(ErrorType.USER_NOT_FOUND);

                throw new NotFoundException(error);
            }

            var session = await _sessionService.GetSessionByUserIdAsync(userId);

            await _sessionService.DeleteSessionAsync(session);

            transaction.Commit();
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }

    private async Task createSession(UserModel user)
    {
        var (accessToken, refreshToken) = await _sessionService.CreateSessionAsync(user);

        user.AccessToken = accessToken;
        user.RefreshToken = refreshToken;
    }

    public async Task<UserModel> getUserOrThrowAsync(string email)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);

        if(user == null)
        {
            var error = _errorFactory.Create(ErrorType.LOGIN_FAILED);

            throw new NotFoundException(error);
        }

        return user;
    }

    public void verifyPassword(string password, string hashedPassword)
    {
        var isCorrectPassword = _hashService.Verify(password, hashedPassword);

        if (isCorrectPassword == false)
        {
            var error = _errorFactory.Create(ErrorType.LOGIN_FAILED);

            throw new NotFoundException(error);
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

    private async Task createUserAsync(GetRoleModel role, UserModel userModel)
    {
        var newUser = UserFactory.CreateUserByRole(role, userModel);

        _userRepository.CreateUser(newUser);

        await _unitOfWork.SaveChangesAsync();

        userModel.UserId = newUser.Id;
        userModel.Role = role.Name;
    }
}