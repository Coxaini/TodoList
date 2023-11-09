namespace ToDoList.Application.Exceptions.Base;

public class AppException : Exception
{
    public AppException(string code, ExceptionType type, string message, Exception? innerException = null) : base(
        message, innerException)
    {
        Code = code;
        Type = type;
    }

    public string Code { get; }
    public ExceptionType Type { get; }
}