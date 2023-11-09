using ToDoList.Application.Models.ToDoTasks;

namespace ToDoList.Application.Services.ToDoTasks;

public interface IToDoTasksService
{
    Task<ToDoTaskDto> CreateTaskAsync(CreateToDoTaskDto dto, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<ToDoTaskDto>> GetTasksAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<ToDoTaskDto> UpdateTaskAsync(UpdateToDoTaskDto dto, CancellationToken cancellationToken = default);
    Task DeleteTaskAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
}