using BookLab.API.Dtos;
using BookLab.Application.Dtos;
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
    private readonly ITokenService tokenService;
    private readonly AuthService _uut;

    private User existingAccount = new() { Email = "Test@gmail.com", Password = "somepassword" };
    private User nonExistingAccount = new() { Email = "duaa@gmail.com", Password = "somepassword", UserName = "duaa" };
    private string invalidRole = "test";

    public AuthServiceTests()
    {
        usersRepository = new UsersRepositoryMocks().UserRepository.Object;
        unitOfWork = new UnitOfWorkMocks().UnitOfWork.Object;
        roleRepository = new RoleRepositoryMocks().RoleRepository.Object;
        hashService = new HashServiceMocks().HashService.Object;
        errorFactory = new ErrorFactory();
        sessionService = new Mock<ISessionService>().Object;
        tokenService = new Mock<ITokenService>().Object;

        _uut = new AuthService
            (usersRepository,
            roleRepository,
            hashService,
            unitOfWork,
            sessionService,
            errorFactory,
            tokenService);
    }

    [Fact]
    public async void CreateUserAsync_AccountAlreadyExists_ThrowConflictException()
    {
        var request = new CreateUserRequest { Email = existingAccount.Email };

        await Assert.ThrowsAsync<ConflictException>(() => _uut.CreateUserAsync(request));
    }

    [Fact]
    public async void CreateUserAsync_RoleDoesnotExist_ThrowNotFoundException()
    {
        var request = new CreateUserRequest { Email = nonExistingAccount.Email, Role = invalidRole };

        await Assert.ThrowsAsync<NotFoundException>(() => _uut.CreateUserAsync(request));
    }

    [Theory]
    [InlineData("Admin")]
    [InlineData("Customer")]
    public async void CreateUserAsync_UserCreatedSuccessfully(string role)
    {
        var request = new CreateUserRequest { Email = nonExistingAccount.Email, Password = nonExistingAccount.Password, Role = role };

        var newUser = await _uut.CreateUserAsync(request);

        Assert.NotNull(newUser);
        Assert.Equal(role, newUser.Role);
        Assert.Equal(nonExistingAccount.UserName, newUser.UserName);
    }

    [Fact]
    public async void LoginUserAsync_UserDoesnotExist_ThrowNotFoundExpection()
    {
        var request = new LoginUserRequest { Email = nonExistingAccount.Email };

        await Assert.ThrowsAsync<NotFoundException>(() => _uut.LoginUserAsync(request));
    }

    [Fact]
    public async void LoginUserAsync_WrongPassword_ThrowNotFoundException()
    {
        var request = new LoginUserRequest { Email = existingAccount.Email, Password = $"{existingAccount.Password}//" };

        await Assert.ThrowsAsync<NotFoundException>(() => _uut.LoginUserAsync(request));
    }

    [Fact]
    public async void LoginUserAsync_ValidCredentials_UserLoggedinSuccessfully()
    {
        var request = new LoginUserRequest { Email = existingAccount.Email, Password = existingAccount.Password };

        var resposne = await _uut.LoginUserAsync(request);

        Assert.NotNull(resposne);
        Assert.Equal(request.Email, resposne.Email);
    }
}
