namespace ToDoList.Application.Models.Authentication;

public record AuthenticationResult(string Token, string RefreshToken, DateTime RefreshTokenExpiryTime);