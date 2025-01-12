using BookLab.Domain.Entities;
using BookLab.Domain.Models;
using BookLab.Infrastructure.Repositories;
using BookLab.Tests.MockData;
using Moq;

namespace BookLab.Tests.Mocks;

public class RoleRepositoryMocks
{
    public Mock<IRoleRepository> RoleRepository { get; set; }
    private List<Role> roles;

    public RoleRepositoryMocks()
    {
        RoleRepository = new Mock<IRoleRepository>();
        roles = new Roles().RolesData;

        RoleRepository.Setup(x => x.GetRoleByName(It.IsAny<string>()))
            .ReturnsAsync((string roleName) =>
            {
                return roles.Select(r => new GetRoleModel
                {
                    Id = r.Id,
                    Name = r.Name,
                })
                .FirstOrDefault(r => r.Name == roleName);
            });
    }
}
