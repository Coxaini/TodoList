namespace ToDoList.Application.Models.ToDoTasks;

public record CreateToDoTaskDto(Guid UserId, string Title);