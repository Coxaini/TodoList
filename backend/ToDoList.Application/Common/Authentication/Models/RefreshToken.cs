namespace ToDoList.Application.Common.Authentication.Models;

public record RefreshToken(string Token = "", DateTime ExpiryTime = default);