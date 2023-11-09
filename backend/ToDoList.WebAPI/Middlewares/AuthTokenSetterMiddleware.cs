namespace ToDoList.WebAPI.Middlewares;

public class AuthTokenSetterMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string? token = context.Request.Cookies["accessToken"];

        if (!string.IsNullOrEmpty(token))
            context.Request.Headers.TryAdd("Authorization", "Bearer " + token);

        context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Add("X-Xss-Protection", "1");
        context.Response.Headers.Add("X-Frame-Options", "DENY");

        await next(context);
    }
}