using Microsoft.OpenApi.Models;
using ToDoList.Application;
using ToDoList.Infrastructure;
using ToDoList.WebAPI.Interfaces;
using ToDoList.WebAPI.Middlewares;
using ToDoList.WebAPI.Services;

namespace ToDoList.WebAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ExceptionHandlerMiddleware>();
        services.AddTransient<AuthTokenSetterMiddleware>();

        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(opt =>
        {
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        services
            .AddApplication()
            .AddInfrastructure(configuration);

        services.AddHttpContextAccessor();
        services.AddScoped<ICookieTokensService, CookieTokensService>();

        return services;
    }
}