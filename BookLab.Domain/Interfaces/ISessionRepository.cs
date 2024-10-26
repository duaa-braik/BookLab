using BookLab.Domain.Entities;

namespace BookLab.Domain.Interfaces;

public interface ISessionRepository
{
    void CreateSession(Session session);
}