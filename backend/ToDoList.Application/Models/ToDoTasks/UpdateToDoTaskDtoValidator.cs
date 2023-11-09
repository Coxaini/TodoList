using FluentValidation;

namespace ToDoList.Application.Models.ToDoTasks;

public class UpdateToDoTaskDtoValidator : AbstractValidator<UpdateToDoTaskDto>
{
    public UpdateToDoTaskDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(300);
    }
}