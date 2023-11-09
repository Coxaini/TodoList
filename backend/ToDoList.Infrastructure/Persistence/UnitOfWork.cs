using ToDoList.Application.Common.Repositories;

namespace ToDoList.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ToDoListDbContext _context;

    public UnitOfWork(ToDoListDbContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}