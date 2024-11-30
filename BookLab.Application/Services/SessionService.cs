using BookLab.Application.Interfaces;
using BookLab.Domain.Entities;
using BookLab.Domain.Interfaces;
using BookLab.Domain.Models;
using static BookLab.Domain.Constants.AuthConstants;

namespace BookLab.Application.Services;

public class SessionService : ISessionService
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHashService _hashService;
    ITokenService _tokenService;

    public SessionService(
        ISessionRepository sessionRepository, 
        IUnitOfWork unitOfWork,
        IHashService hashService,
        ITokenService tokenService)
    {
        _sessionRepository = sessionRepository;
        _unitOfWork = unitOfWork;
        _hashService = hashService;
        _tokenService = tokenService;
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
}
