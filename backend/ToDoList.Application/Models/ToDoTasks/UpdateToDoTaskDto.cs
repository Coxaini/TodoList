namespace ToDoList.Application.Models.ToDoTasks;

public record UpdateToDoTaskDto(Guid Id, Guid UserId, string Title, bool IsCompleted);