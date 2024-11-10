using BookLab.Domain.Entities;
using BookLab.Domain.Interfaces;
using BookLab.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLab.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BookLabDbContext _context;

    public UserRepository(BookLabDbContext context)
    {
        _context = context;
    }

    public void CreateUser<TUser>(TUser user) where TUser : class
    {
        _context.Set<TUser>().Add(user);
    }

    public async Task<string?> GetUserEmailAsync(string email)
    {
        return await _context.User
            .Where(u => u.Email == email)
            .Select(u => u.Email)
            .FirstOrDefaultAsync();
    }

    public async Task<UserModel?> GetUserByEmailAsync(string email)
    {
        return await _context.User
            .Include(u => u.Role)
            .Where(u => u.Email == email)
            .Select(u => new UserModel
            {
                UserId = u.Id,
                Email = u.Email,
                Password = u.Password,
                Role = u.Role.Name,
            })
            .FirstOrDefaultAsync();
    }
}
