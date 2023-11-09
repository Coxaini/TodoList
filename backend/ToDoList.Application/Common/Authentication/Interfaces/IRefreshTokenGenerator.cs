using ToDoList.Application.Common.Authentication.Models;

namespace ToDoList.Application.Common.Authentication.Interfaces;

public interface IRefreshTokenGenerator
{
    public RefreshToken GenerateRefreshToken();
}