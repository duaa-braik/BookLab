using BookLab.Domain.Entities;
using BookLab.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookLab.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BookLabDbContext _context;

    public UserRepository(BookLabDbContext context)
    {
        _context = context;
    }

    public void CreateUser(User user)
    {
        _context.User.Add(user);
    }

    public async Task<string?> GetUserEmailAsync(string email)
    {
        return await _context.User
            .Where(u => u.Email == email)
            .Select(u => u.Email)
            .FirstOrDefaultAsync();
    }
}
