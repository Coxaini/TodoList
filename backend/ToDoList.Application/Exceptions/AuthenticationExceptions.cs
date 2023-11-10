using ToDoList.Application.Exceptions.Base;

namespace ToDoList.Application.Exceptions;

public static class AuthenticationExceptions
{
    public static readonly Exception UserAlreadyExists =
        new AppException("User.AlreadyExists", ExceptionType.Conflict, "User already exists");

    public static readonly Exception IncorrectPassword =
        new AppException("User.IncorrectPassword", ExceptionType.Validation, "Incorrect password");

    public static readonly Exception InvalidRefreshToken =
        new AppException("User.InvalidRefreshToken", ExceptionType.Validation, "Invalid refresh token");

    public static Exception InvalidToken(Exception innerException)
    {
        return new AppException("User.InvalidToken", ExceptionType.Validation, "Invalid token", innerException);
    }
}