using BookLab.Domain.Models.Role;

namespace BookLab.Infrastructure.Repositories;

public interface IRoleRepository
{
    Task<GetRoleModel> GetRoleByName(string roleName);
}