using System.Security.Claims;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Common.Authentication.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}