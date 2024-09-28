using BookLab.Application.Interfaces;
using Bcrypt = BCrypt.Net.BCrypt;

namespace BookLab.Application.Utils;

public class PasswordHasher : IPasswordHasher
{
    private const int WORK_FACTOR = 13;
    public string Hash(string password)
    {
        return Bcrypt.EnhancedHashPassword(password, WORK_FACTOR);
    }
}
