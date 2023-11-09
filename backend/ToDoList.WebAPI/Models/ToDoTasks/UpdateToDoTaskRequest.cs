namespace ToDoList.WebAPI.Models.ToDoTasks;

public record UpdateToDoTaskRequest(string Title, bool IsCompleted);