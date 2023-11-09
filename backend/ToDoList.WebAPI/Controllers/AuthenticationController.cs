using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Models.Authentication;
using ToDoList.Application.Services.Authentication;
using ToDoList.WebAPI.Interfaces;

namespace ToDoList.WebAPI.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly ICookieTokensService _cookieTokensService;

    public AuthenticationController(IAuthenticationService authenticationService,
        ICookieTokensService cookieTokensService)
    {
        _authenticationService = authenticationService;
        _cookieTokensService = cookieTokensService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthenticationRequest request)
    {
        var result = await _authenticationService.LoginAsync(request);

        _cookieTokensService.SetAccessTokenCookie(result.Token, result.RefreshTokenExpiryTime);
        _cookieTokensService.SetRefreshTokenCookie(result.RefreshToken, result.RefreshTokenExpiryTime);

        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthenticationRequest request)
    {
        var result = await _authenticationService.RegisterAsync(request);

        _cookieTokensService.SetAccessTokenCookie(result.Token, result.RefreshTokenExpiryTime);
        _cookieTokensService.SetRefreshTokenCookie(result.RefreshToken, result.RefreshTokenExpiryTime);

        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        string? refreshToken = Request.Cookies["refreshToken"];
        if (string.IsNullOrEmpty(refreshToken)) return Unauthorized("Refresh token is missing");

        string accessToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        if (string.IsNullOrEmpty(accessToken)) return Unauthorized("Access token is missing");

        var result = await _authenticationService.RefreshTokenAsync(new RefreshTokenRequest(accessToken, refreshToken));

        _cookieTokensService.SetAccessTokenCookie(result.Token, result.RefreshTokenExpiryTime);
        _cookieTokensService.SetRefreshTokenCookie(result.RefreshToken, result.RefreshTokenExpiryTime);

        return Ok();
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        _cookieTokensService.RemoveAccessTokenCookie();
        _cookieTokensService.RemoveRefreshTokenCookie();

        return Ok();
    }
}