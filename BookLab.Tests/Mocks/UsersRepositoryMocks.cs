using BookLab.Domain.Entities;
using BookLab.Domain.Interfaces;
using BookLab.Tests.MockData;
using Moq;

namespace BookLab.Tests.Mocks;

public class UsersRepositoryMocks
{
    private List<User> _users;
    public Mock<IUserRepository> UserRepository { get; set; }

    public UsersRepositoryMocks()
    {
        _users = new Users().UsersData;
        UserRepository = new Mock<IUserRepository>();

        UserRepository.Setup(x => x.GetUserEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((string email) =>
            {
                return _users.Select(u => u.Email).FirstOrDefault(e => e == email);
            });

        UserRepository.Setup(x => x.CreateUser(It.IsAny<Customer>()))
            .Callback<Customer>(e =>
            {
                _users.Add(e);
            });

        UserRepository.Setup(x => x.CreateUser(It.IsAny<Admin>()))
            .Callback<Admin>(e =>
            {
                _users.Add(e);
            });
    }
}
