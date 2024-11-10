namespace BookLab.Application.Interfaces;

public interface IHashService
{
    string Hash(string text);
    bool Verify(string text, string hash);
}