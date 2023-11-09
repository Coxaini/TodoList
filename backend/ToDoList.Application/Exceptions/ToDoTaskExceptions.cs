using ToDoList.Application.Exceptions.Base;

namespace ToDoList.Application.Exceptions;

public static class ToDoTaskExceptions
{
    public static readonly Exception ToDoTaskNotFound =
        new AppException("ToDoTask.NotFound", ExceptionType.NotFound, "ToDoTask is not found");
}