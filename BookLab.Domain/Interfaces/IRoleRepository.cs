using BookLab.Domain.Models;

namespace BookLab.Infrastructure.Repositories;

public interface IRoleRepository
{
    Task<int> GetRoleIdByName(string roleName);
}