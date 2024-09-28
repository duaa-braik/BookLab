using BookLab.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLab.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly BookLabDbContext _context;

    public RoleRepository(BookLabDbContext context)
    {
        _context = context;
    }

    public async Task<GetRoleModel> GetRoleByName(string roleName)
    {
        return await _context.Role
            .Where(r => r.Name == roleName)
            .Select(r => new GetRoleModel { Id = r.Id, Name = r.Name })
            .FirstOrDefaultAsync();
    }
}
