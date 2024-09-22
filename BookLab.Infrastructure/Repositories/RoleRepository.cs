using Microsoft.EntityFrameworkCore;

namespace BookLab.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly BookLabDbContext _context;

    public RoleRepository(BookLabDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetRoleIdByName(string roleName)
    {
        return await _context.Role
            .Where(r => r.Name == roleName)
            .Select(r => r.Id)
            .FirstOrDefaultAsync();
    }
}
