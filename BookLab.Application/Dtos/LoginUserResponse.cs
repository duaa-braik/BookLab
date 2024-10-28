namespace BookLab.Application.Dtos;

public class LoginUserResponse
{
    public string UserId { get; set; }

    public string Email { get; set; }

    public string Role { get; set; }

    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
}
