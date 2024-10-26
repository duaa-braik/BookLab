using BookLab.Domain.Entities;

namespace BookLab.Tests.MockData;

public class Users
{
    public List<User> UsersData { get; } = [];

    public Users()
    {
        UsersData = 
        [
            new Customer {
                Id = Guid.NewGuid(),
                UserName = "Test1",
                Email = "Test1@gmail.com",
                Password = "somepassword",
                RoleId = 1,
            },
            new Admin 
            {
                Id = Guid.NewGuid(),
                UserName = "Test",
                Email = "Test@gmail.com",
                Password = "somepassword",
                RoleId = 2,
            }
        ];
    }
}
