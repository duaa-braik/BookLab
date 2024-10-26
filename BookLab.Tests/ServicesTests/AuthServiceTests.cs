using BookLab.API.Dtos;
using BookLab.Application.Factories;
using BookLab.Application.Interfaces;
using BookLab.Application.Services;
using BookLab.Domain.Entities;
using BookLab.Domain.Exceptions;
using BookLab.Domain.Interfaces;
using BookLab.Infrastructure.Repositories;
using BookLab.Tests.Mocks;
using Moq;

namespace BookLab.Tests.ServicesTests;

public class AuthServiceTests
{
    private readonly IUserRepository usersRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IRoleRepository roleRepository;
    private readonly IHashService hashService;
    private readonly ISessionService sessionService;
    private readonly IErrorFactory errorFactory;
    private readonly AuthService _uut;

    public AuthServiceTests()
    {
        usersRepository = new UsersRepositoryMocks().UserRepository.Object;
        unitOfWork = new UnitOfWorkMocks().UnitOfWork.Object;
        roleRepository = new RoleRepositoryMocks().RoleRepository.Object;
        hashService = new Mock<IHashService>().Object;
        errorFactory = new ErrorFactory();
        sessionService = new Mock<ISessionService>().Object;

        _uut = new AuthService
            (usersRepository, 
            roleRepository, 
            hashService, 
            unitOfWork, 
            sessionService, 
            errorFactory);
    }

    [Fact]
    public async void CreateUserAsync_AccountAlreadyExists_ThrowConflictException()
    {
        var request = new CreateUserRequest { Email = "Test@gmail.com" };

        await Assert.ThrowsAsync<ConflictException>(() => _uut.CreateUserAsync(request));
    }

    [Fact]
    public async void CreateUserAsync_RoleDoesnotExist_ThrowNotFoundException()
    {
        var request = new CreateUserRequest { Email = "duaa@gmail.com", Role = "test" };

        await Assert.ThrowsAsync<NotFoundException>(() => _uut.CreateUserAsync(request));
    }

    [Theory]
    [InlineData("Admin")]
    [InlineData("Customer")]
    public async void CreateUserAsync_UserCreatedSuccessfully(string role)
    {
        var request = new CreateUserRequest { Email = "duaa@gmail.com", Password = "somepassword", Role = role };

        var newUser = await _uut.CreateUserAsync(request);

        Assert.NotNull(newUser);
        Assert.Equal(role, newUser.Role);
        Assert.Equal("duaa", newUser.UserName);
    }
}
