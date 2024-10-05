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

namespace BookLab.Application.Services;

public class TokenGeneratorService : ITokenGeneratorService
{
    private readonly JwtConfig jwtConfig;

    public TokenGeneratorService(IOptions<JwtConfig> options)
    {
        jwtConfig = options.Value;
    }

    public string Generate(CreateUserModel user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(jwtConfig.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity([
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(JwtRegisteredClaimNames.Iss, jwtConfig.Issuer),
                new Claim(JwtRegisteredClaimNames.Aud, jwtConfig.Audience),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("role", user.Role),
            ]),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}