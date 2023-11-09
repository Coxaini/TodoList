using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Models.ToDoTasks;
using ToDoList.Application.Services.ToDoTasks;
using ToDoList.WebAPI.Controllers.Base;
using ToDoList.WebAPI.Models.ToDoTasks;

namespace ToDoList.WebAPI.Controllers;

[Route("tasks")]
public class ToDoTasksController : ApiController
{
    private readonly IToDoTasksService _toDoTaskService;

    public ToDoTasksController(IToDoTasksService toDoTaskService)
    {
        _toDoTaskService = toDoTaskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks(CancellationToken cancellationToken = default)
    {
        var result = await _toDoTaskService.GetTasksAsync(UserId, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateToDoTaskRequest request,
        CancellationToken cancellationToken = default)
    {
        var result =
            await _toDoTaskService.CreateTaskAsync(new CreateToDoTaskDto(UserId, request.Title), cancellationToken);

        return Ok(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UpdateToDoTaskRequest request,
        CancellationToken cancellationToken = default)
    {
        var result =
            await _toDoTaskService.UpdateTaskAsync(
                new UpdateToDoTaskDto(id, UserId, request.Title, request.IsCompleted), cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTask(Guid id, CancellationToken cancellationToken = default)
    {
        await _toDoTaskService.DeleteTaskAsync(id, UserId, cancellationToken);

        return NoContent();
    }
}