namespace ToDoList.Application.Models.ToDoTasks;

public record ToDoTaskDto(Guid Id, string Title, bool IsCompleted, DateTime CreatedAt, DateTime? CompletedAt);