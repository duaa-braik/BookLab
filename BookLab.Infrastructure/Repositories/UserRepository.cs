using BookLab.Domain.Entities;
using BookLab.Domain.Interfaces;

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
}
