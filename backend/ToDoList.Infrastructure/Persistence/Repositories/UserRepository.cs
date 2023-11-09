using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Common.Repositories;
using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbSet<User> _users;

    public UserRepository(ToDoListDbContext context)
    {
        _users = context.Users;
    }

    public async Task<bool> IsUserExistAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _users.AnyAsync(x => x.Username == username, cancellationToken);
    }

    public async Task<User?> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _users.FirstOrDefaultAsync(x => x.Username == username, cancellationToken);
    }

    public async Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Add(User user)
    {
        _users.Add(user);
    }
}