using ToDoList.Domain.Entities;

namespace ToDoList.Application.Common.Repositories;

public interface IUserRepository
{
    Task<bool> IsUserExistAsync(string username, CancellationToken cancellationToken = default);
    Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(User user);
}