using FluentValidation;
using ToDoList.Application.Common.Repositories;
using ToDoList.Application.Exceptions;
using ToDoList.Application.Models.ToDoTasks;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Services.ToDoTasks;

public class ToDoTasksService : IToDoTasksService
{
    private readonly IValidator<CreateToDoTaskDto> _createToDoTaskValidator;
    private readonly IToDoTaskRepository _toDoTaskRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateToDoTaskDto> _updateToDoTaskValidator;

    public ToDoTasksService(IToDoTaskRepository toDoTaskRepository, IUnitOfWork unitOfWork,
        IValidator<CreateToDoTaskDto> createToDoTaskValidator,
        IValidator<UpdateToDoTaskDto> updateToDoTaskValidator)
    {
        _toDoTaskRepository = toDoTaskRepository;
        _unitOfWork = unitOfWork;
        _createToDoTaskValidator = createToDoTaskValidator;
        _updateToDoTaskValidator = updateToDoTaskValidator;
    }

    public async Task<ToDoTaskDto> CreateTaskAsync(CreateToDoTaskDto dto,
        CancellationToken cancellationToken = default)
    {
        _createToDoTaskValidator.ValidateAndThrow(dto);

        var todoTask = ToDoTask.Create(dto.UserId, dto.Title);

        _toDoTaskRepository.Add(todoTask);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ToDoTaskDto(todoTask.Id, todoTask.Title, todoTask.IsCompleted, todoTask.CreatedAt,
            todoTask.CompletedAt);
    }

    public async Task<IReadOnlyCollection<ToDoTaskDto>> GetTasksAsync(Guid userId,
        CancellationToken cancellationToken = default)
    {
        var tasks = await _toDoTaskRepository.GetAllByUserIdAsync(userId, cancellationToken);

        return tasks.Select(x => new ToDoTaskDto(x.Id, x.Title, x.IsCompleted, x.CreatedAt, x.CompletedAt))
            .ToList();
    }

    public async Task<ToDoTaskDto> UpdateTaskAsync(UpdateToDoTaskDto dto,
        CancellationToken cancellationToken = default)
    {
        _updateToDoTaskValidator.ValidateAndThrow(dto);

        var task = await _toDoTaskRepository.GetByIdAsync(dto.Id, cancellationToken);

        if (task is null || task.UserId != dto.UserId)
            throw ToDoTaskExceptions.ToDoTaskNotFound;

        task.UpdateTitle(dto.Title);

        if (dto.IsCompleted && !task.IsCompleted)
            task.MarkComplete();
        else if (!dto.IsCompleted && task.IsCompleted)
            task.MarkIncomplete();

        _toDoTaskRepository.Update(task);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ToDoTaskDto(task.Id, task.Title, task.IsCompleted, task.CreatedAt, task.CompletedAt);
    }

    public async Task DeleteTaskAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
    {
        var task = await _toDoTaskRepository.GetByIdAsync(id, cancellationToken);

        if (task is null || task.UserId != userId)
            throw ToDoTaskExceptions.ToDoTaskNotFound;

        _toDoTaskRepository.Remove(task);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}