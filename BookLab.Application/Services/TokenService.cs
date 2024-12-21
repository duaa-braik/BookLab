using BookLab.API.Dtos;
using BookLab.Application.Configurations;
using BookLab.Application.Factories;
using BookLab.Application.Interfaces;
using BookLab.Domain.Entities;
using BookLab.Domain.Exceptions;
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
    private readonly IErrorFactory _errorFactory;
    private readonly JwtConfig jwtConfig;
    private byte[] key;

    public TokenService(IOptions<JwtConfig> options, IErrorFactory errorFactory)
    {
        jwtConfig = options.Value;
        key = Encoding.UTF8.GetBytes(jwtConfig.Secret);
        _errorFactory = errorFactory;
    }

    public string Generate(UserModel user, TokenType tokenType)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

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

    public bool Validate(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidAudience = jwtConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            }, out var _);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public string GetClaimFromJwtToken(string token, ClaimType type)
    {
        var claimType = Claims[type];

        return new JwtSecurityToken(token).Claims.First(c => c.Type == claimType).Value;
    }
}