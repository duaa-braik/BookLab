using BookLab.Application.Interfaces;
using Bcrypt = BCrypt.Net.BCrypt;

namespace BookLab.Application.Utils;

public class HashService : IHashService
{
    private const int WORK_FACTOR = 13;
    public string Hash(string text)
    {
        return Bcrypt.EnhancedHashPassword(text, WORK_FACTOR);
    }

    public bool Verify(string text, string hash)
    {
        return Bcrypt.EnhancedVerify(text, hash);
    }
}
