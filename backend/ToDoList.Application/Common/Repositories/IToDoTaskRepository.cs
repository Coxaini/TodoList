using ToDoList.Domain.Entities;

namespace ToDoList.Application.Common.Repositories;

public interface IToDoTaskRepository
{
    public void Add(ToDoTask toDoTask);
    public void Remove(ToDoTask toDoTask);

    public Task<IReadOnlyCollection<ToDoTask>> GetAllByUserIdAsync(Guid userId,
        CancellationToken cancellationToken = default);

    public Task<ToDoTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    public void Update(ToDoTask toDoTask);
}