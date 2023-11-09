using FluentValidation;

namespace ToDoList.Application.Models.ToDoTasks;

public class CreateToDoTaskDtoValidator : AbstractValidator<CreateToDoTaskDto>
{
    public CreateToDoTaskDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(300);
    }
}