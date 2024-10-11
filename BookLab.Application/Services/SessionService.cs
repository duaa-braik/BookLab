using BookLab.Application.Interfaces;
using BookLab.Domain.Entities;
using BookLab.Domain.Interfaces;

namespace BookLab.Application.Services;

public class SessionService : ISessionService
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SessionService(ISessionRepository sessionRepository, IUnitOfWork unitOfWork)
    {
        _sessionRepository = sessionRepository;
        this._unitOfWork = unitOfWork;
    }

    public async Task CreateSessionAsync(User user, string refreshToken)
    {
        var session = new Session
        {
            UserId = user.Id,
            RefreshToken = refreshToken, // hashed
            LastLogin = DateTime.UtcNow,
        };

        _sessionRepository.CreateSession(session);

        await _unitOfWork.SaveChangesAsync();
    }
}
