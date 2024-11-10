using BookLab.Domain.Entities;
using BookLab.Domain.Models;

namespace BookLab.Domain.Interfaces
{
    public interface IUserRepository
    {
        void CreateUser<TUser>(TUser user) where TUser : class;
        Task<UserModel?> GetUserByEmailAsync(string email);
        Task<string?> GetUserEmailAsync(string email);
    }
}
