using BookLab.Domain.Entities;
using BookLab.Domain.Models;
using static BookLab.Domain.Constants.AuthConstants;

namespace BookLab.Application.Factories.UserFactory;

public class UserFactory
{
    private const bool ignoreRoleTypeCase = true;

    public static User CreateUserByRole(GetRoleModel role, CreateUserModel createUserModel)
    {
        Enum.TryParse(typeof(RoleType), role.Name, ignoreRoleTypeCase, out var roleType);

        switch (roleType)
        {
            case RoleType.Customer:
                return new Customer
                {
                    Email = createUserModel.Email,
                    UserName = createUserModel.UserName,
                    Password = createUserModel.Password,
                    RoleId = role.Id,
                    CreatedAt = DateTime.Now,
                };

            case RoleType.Admin:
                return new Admin
                {
                    Email = createUserModel.Email,
                    UserName = createUserModel.UserName,
                    Password = createUserModel.Password,
                    RoleId = role.Id,
                    CreatedAt = DateTime.Now,
                };

            default:
                throw new Exception();
        }
    }
}
