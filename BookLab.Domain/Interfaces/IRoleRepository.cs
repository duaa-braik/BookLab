using BookLab.Domain.Models;

namespace BookLab.Infrastructure.Repositories;

public interface IRoleRepository
{
    Task<GetRoleModel> GetRoleByName(string roleName);
}