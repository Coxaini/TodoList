using ToDoList.Application.Exceptions.Base;

namespace ToDoList.Application.Exceptions;

public static class UserExceptions
{
    public static readonly AppException
        UserNotFound = new("User.NotFound", ExceptionType.NotFound, "User is not found");
}