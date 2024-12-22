using BookLab.Application.Factories;
using BookLab.Application.Interfaces;
using BookLab.Domain.Entities;
using BookLab.Domain.Exceptions;
using BookLab.Domain.Interfaces;
using BookLab.Domain.Models;
using Mapster;
using static BookLab.Domain.Constants.AuthConstants;
using static BookLab.Domain.Constants.ErrorConstants;

namespace BookLab.Application.Services;

public class SessionService : ISessionService
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHashService _hashService;
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;
    private readonly IErrorFactory _errorFactory;


    public SessionService(
        ISessionRepository sessionRepository, 
        IUnitOfWork unitOfWork,
        IHashService hashService,
        ITokenService tokenService,
        IUserRepository userRepository,
        IErrorFactory errorFactory)
    {
        _sessionRepository = sessionRepository;
        _unitOfWork = unitOfWork;
        _hashService = hashService;
        _tokenService = tokenService;
        _userRepository = userRepository;
        _errorFactory = errorFactory;
    }

    public async Task<(string, string)> CreateSessionAsync(UserModel user)
    {
        var accessToken = _tokenService.Generate(user, TokenType.ACCESS_TOKEN);
        var refreshToken = _tokenService.Generate(user, TokenType.REFRESH_TOKEN);

        var session = new Session
        {
            UserId = user.UserId,
            RefreshToken = _hashService.Hash(refreshToken),
            LastLogin = DateTime.UtcNow,
        };

        _sessionRepository.CreateSession(session);

        await _unitOfWork.SaveChangesAsync();

        return (accessToken, refreshToken);
    }

    public async Task<SessionModel> RefreshSessionAsync(string token)
    {
        var userId = await getUserIdFromClaims(token);

        var session = await _sessionRepository.GetSessionByUserIdAsync(new Guid(userId));

        bool isValidToken = _tokenService.Validate(token);

        if (session == null || !isValidToken)
        {
            await handleUnauthorizedUser(session);
        }

        var isSameToken = _hashService.Verify(token, session.RefreshToken);

        if (!isSameToken)
        {
            await handleUnauthorizedUser(session);
        }

        var userModel = await updateSession(userId, session);

        return userModel.Adapt<SessionModel>();
    }

    public async Task<Session> GetSessionByUserIdAsync(string userId)
    {
        return await _sessionRepository.GetSessionByUserIdAsync(new Guid(userId));
    }

    public async Task DeleteSessionAsync(Session? session)
    {
        if (session is null) return;

        _sessionRepository.DeleteSession(session);

        await _unitOfWork.SaveChangesAsync();
    }

    private async Task<UserModel> updateSession(string userId, Session? session)
    {
        var user = await _userRepository.GetUserByUserId(new Guid(userId));

        var refreshToken = _tokenService.Generate(user, TokenType.REFRESH_TOKEN);

        session!.RefreshToken = _hashService.Hash(refreshToken);

        _sessionRepository.UpdateSession(session);

        await _unitOfWork.SaveChangesAsync();

        user.RefreshToken = refreshToken;
        user.AccessToken = _tokenService.Generate(user, TokenType.ACCESS_TOKEN);

        return user;
    }

    private async Task handleUnauthorizedUser(Session? session)
    {
        await DeleteSessionAsync(session);

        var error = _errorFactory.Create(ErrorType.INVALID_TOKEN);

        throw new UnauthorizedException(error);
    }

    private async Task<string> getUserIdFromClaims(string token)
    {
        string userId = string.Empty;

        try
        {
            userId = _tokenService.GetClaimFromJwtToken(token, ClaimType.UserId);
        }
        catch(Exception)
        {
            await handleUnauthorizedUser(null);
        }

        return userId;
    }
}
