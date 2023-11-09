using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Application.Services.Authentication;
using ToDoList.Application.Services.ToDoTasks;

namespace ToDoList.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IToDoTasksService, ToDoTasksService>();

        return services;
    }
}