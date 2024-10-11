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
    ITokenGeneratorService _tokenGeneratorService;

    public SessionService(
        ISessionRepository sessionRepository, 
        IUnitOfWork unitOfWork,
        IHashService hashService,
        ITokenGeneratorService tokenGeneratorService)
    {
        _sessionRepository = sessionRepository;
        _unitOfWork = unitOfWork;
        _hashService = hashService;
        _tokenGeneratorService = tokenGeneratorService;
    }

    public async Task<(string, string)> CreateSessionAsync(CreateUserModel user)
    {
        var accessToken = _tokenGeneratorService.Generate(user, TokenType.ACCESS_TOKEN);
        var refreshToken = _tokenGeneratorService.Generate(user, TokenType.REFRESH_TOKEN);

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
