using ToDoList.Domain.Common.Base;

namespace ToDoList.Domain.Entities;

public class User : BaseEntity<Guid>
{
    private readonly List<ToDoTask> _toDoTasks = new();

    //For EF
    private User()
    {
    }

    public string Username { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string RefreshToken { get; private set; } = string.Empty;
    public DateTime RefreshTokenExpiryTime { get; private set; }

    public IReadOnlyList<ToDoTask> ToDoTasks => _toDoTasks.AsReadOnly();

    public static User Create(string username, string passwordHash)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Username = username,
            PasswordHash = passwordHash
        };
    }

    public void SetRefreshToken(string refreshToken, DateTime refreshTokenExpiryTime)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpiryTime = refreshTokenExpiryTime;
    }
}