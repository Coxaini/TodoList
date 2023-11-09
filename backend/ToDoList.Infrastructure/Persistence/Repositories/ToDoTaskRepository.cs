using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Common.Repositories;
using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure.Persistence.Repositories;

public class ToDoTaskRepository : IToDoTaskRepository
{
    private readonly DbSet<ToDoTask> _toDoTasks;

    public ToDoTaskRepository(ToDoListDbContext dbContext)
    {
        _toDoTasks = dbContext.ToDoTasks;
    }

    public void Add(ToDoTask toDoTask)
    {
        _toDoTasks.Add(toDoTask);
    }

    public void Remove(ToDoTask toDoTask)
    {
        _toDoTasks.Remove(toDoTask);
    }

    public async Task<IReadOnlyCollection<ToDoTask>> GetAllByUserIdAsync(Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _toDoTasks
            .Where(x => x.UserId == userId)
            .OrderBy(x => x.IsCompleted)
            .ThenByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<ToDoTask?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _toDoTasks.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Update(ToDoTask toDoTask)
    {
        _toDoTasks.Update(toDoTask);
    }
}