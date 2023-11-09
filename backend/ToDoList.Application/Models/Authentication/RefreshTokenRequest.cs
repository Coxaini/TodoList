namespace ToDoList.Application.Models.Authentication;

public record RefreshTokenRequest(string ExpiredToken, string RefreshToken);