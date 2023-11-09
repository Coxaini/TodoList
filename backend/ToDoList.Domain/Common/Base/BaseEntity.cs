namespace ToDoList.Domain.Common.Base;

public class BaseEntity<T>
{
    public T Id { get; protected init; }
}