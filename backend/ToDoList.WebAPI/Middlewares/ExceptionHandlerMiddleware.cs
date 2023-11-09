using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Exceptions.Base;

namespace ToDoList.WebAPI.Middlewares;

public class ExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (AppException ex)
        {
            var code = ex.Type switch
            {
                ExceptionType.NotFound => HttpStatusCode.NotFound,
                ExceptionType.Conflict => HttpStatusCode.Conflict,
                ExceptionType.Validation => HttpStatusCode.BadRequest,
                ExceptionType.AccessDenied => HttpStatusCode.Forbidden,
                ExceptionType.Unexpected => HttpStatusCode.InternalServerError,
                _ => HttpStatusCode.InternalServerError
            };

            context.Response.StatusCode = (int)code;
            context.Response.ContentType = "application/json";

            ProblemDetails problemDetails = new()
            {
                Title = ex.Message,
                Type = ex.Code,
                Status = (int)code,
                Detail = ex.Message
            };

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
        catch (ValidationException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var errors = ex.Errors.Select(e => new
            {
                e.PropertyName,
                e.ErrorMessage
            });

            await context.Response.WriteAsJsonAsync(errors);
        }
    }
}