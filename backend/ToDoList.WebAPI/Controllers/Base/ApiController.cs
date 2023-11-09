using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.WebAPI.Controllers.Base;

[ApiController]
[Authorize]
public class ApiController : ControllerBase
{
    protected Guid UserId
    {
        get
        {
            string? userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool isParsed = Guid.TryParse(userId, out var result);

            return !isParsed ? throw new InvalidOperationException("Cannot find user id in claims") : result;
        }
    }
}