using BookLab.Domain.Entities;
using BookLab.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Session?> GetSessionByUserIdAsync(Guid userId)
    {
        return await _context.Session.FirstOrDefaultAsync(s => s.UserId == userId);
    }

    public void DeleteSession(Session session)
    {
        _context.Session.Remove(session);
    }

    public void UpdateSession(Session session)
    {
        _context.Session.Attach(session);
        _context.Entry(session).Property(s => s.RefreshToken).IsModified = true;
    }
}
