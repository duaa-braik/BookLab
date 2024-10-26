using BookLab.Domain.Entities;

namespace BookLab.Tests.MockData;

public class Roles
{
    public List<Role> RolesData { get; set; }

    public Roles()
    {
        RolesData = [
            new Role
            {
                Id = 1,
                Name = "Customer"
            },
            new Role
            {
                Id = 2,
                Name = "Admin"
            }
        ];
    }
}