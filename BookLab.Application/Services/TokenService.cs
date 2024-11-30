using BookLab.API.Dtos;
using BookLab.Application.Configurations;
using BookLab.Application.Interfaces;
using BookLab.Domain.Entities;
using BookLab.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static BookLab.Domain.Constants.AuthConstants;

namespace BookLab.Application.Services;

public class TokenService : ITokenService
{
    private readonly JwtConfig jwtConfig;

    public TokenService(IOptions<JwtConfig> options)
    {
        jwtConfig = options.Value;
    }

    public string Generate(UserModel user, TokenType tokenType)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(jwtConfig.Secret);

        var daysUntilExpired = Expiration[tokenType];

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddDays(daysUntilExpired),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity([
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, jwtConfig.Issuer),
                new Claim(JwtRegisteredClaimNames.Aud, jwtConfig.Audience),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("role", user.Role),
                new Claim("userId", user.UserId.ToString()),
            ]),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}