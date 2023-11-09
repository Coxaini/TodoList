using ToDoList.Application.Models.Authentication;

namespace ToDoList.Application.Services.Authentication;

public interface IAuthenticationService
{
    Task<AuthenticationResult> RegisterAsync(AuthenticationRequest request,
        CancellationToken cancellationToken = default);

    Task<AuthenticationResult> LoginAsync(AuthenticationRequest request, CancellationToken cancellationToken = default);

    Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenRequest request,
        CancellationToken cancellationToken = default);
}