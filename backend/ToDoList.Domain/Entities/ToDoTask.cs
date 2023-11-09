using ToDoList.Domain.Common.Base;

namespace ToDoList.Domain.Entities;

public class ToDoTask : BaseEntity<Guid>
{
    //For EF
    private ToDoTask()
    {
    }

    public Guid UserId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public bool IsCompleted { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    public User User { get; private set; } = null!;

    public static ToDoTask Create(Guid userId, string title)
    {
        return new ToDoTask
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Title = title,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };
    }

    public void UpdateTitle(string title)
    {
        Title = title;
    }

    public void MarkComplete()
    {
        if (IsCompleted)
            throw new InvalidOperationException("Task is already completed");

        IsCompleted = true;
        CompletedAt = DateTime.UtcNow;
    }

    public void MarkIncomplete()
    {
        if (!IsCompleted)
            throw new InvalidOperationException("Task is already incomplete");

        IsCompleted = false;
        CompletedAt = null;
    }
}