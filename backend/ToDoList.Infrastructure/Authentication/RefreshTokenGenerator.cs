using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using ToDoList.Application.Common.Authentication.Interfaces;
using ToDoList.Application.Common.Authentication.Models;

namespace ToDoList.Infrastructure.Authentication;

public class RefreshTokenGenerator : IRefreshTokenGenerator
{
    private readonly IOptions<JwtSettings> _jwtSettings;

    public RefreshTokenGenerator(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    public RefreshToken GenerateRefreshToken()
    {
        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            ExpiryTime = DateTime.UtcNow.AddMinutes(_jwtSettings.Value.RefreshTokenExpiryMinutes)
        };

        return refreshToken;
    }
}