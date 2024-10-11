using BookLab.Application.Interfaces;
using BookLab.Domain.Entities;
using BookLab.Domain.Interfaces;

namespace BookLab.Application.Services;

public class SessionService : ISessionService
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHashService _hashService;

    public SessionService(
        ISessionRepository sessionRepository, 
        IUnitOfWork unitOfWork,
        IHashService hashService)
    {
        _sessionRepository = sessionRepository;
        _unitOfWork = unitOfWork;
        _hashService = hashService;
    }

    public async Task CreateSessionAsync(User user, string refreshToken)
    {
        var session = new Session
        {
            UserId = user.Id,
            RefreshToken = _hashService.Hash(refreshToken),
            LastLogin = DateTime.UtcNow,
        };

        _sessionRepository.CreateSession(session);

        await _unitOfWork.SaveChangesAsync();
    }
}
