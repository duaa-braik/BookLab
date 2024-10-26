using BookLab.Domain.Entities;
using BookLab.Domain.Interfaces;

namespace BookLab.Infrastructure.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly BookLabDbContext _context;

    public SessionRepository(BookLabDbContext context)
    {
        _context = context;
    }

    public void CreateSession(Session session)
    {
        _context.Session.Add(session);
    }
}
