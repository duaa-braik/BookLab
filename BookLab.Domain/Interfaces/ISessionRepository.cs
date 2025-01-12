using BookLab.Domain.Entities;

namespace BookLab.Domain.Interfaces;

public interface ISessionRepository
{
    void CreateSession(Session session);
    void DeleteSession(Session session);
    Task<Session?> GetSessionByUserIdAsync(Guid userId);
    void UpdateSession(Session session);
}