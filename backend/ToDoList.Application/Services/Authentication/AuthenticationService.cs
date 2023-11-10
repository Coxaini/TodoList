using System.Security.Claims;
using FluentValidation;
using ToDoList.Application.Common.Authentication.Interfaces;
using ToDoList.Application.Common.Repositories;
using ToDoList.Application.Exceptions;
using ToDoList.Application.Models.Authentication;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IValidator<AuthenticationRequest> _authenticationRequestValidator;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IRefreshTokenGenerator refreshTokenGenerator,
        IUnitOfWork unitOfWork, IUserRepository userRepository,
        IValidator<AuthenticationRequest> authenticationRequestValidator, IPasswordHasher passwordHasher)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _authenticationRequestValidator = authenticationRequestValidator;
        _passwordHasher = passwordHasher;
    }


    public async Task<AuthenticationResult> RegisterAsync(AuthenticationRequest request,
        CancellationToken cancellationToken = default)
    {
        _authenticationRequestValidator.ValidateAndThrow(request);

        if (await _userRepository.IsUserExistAsync(request.Username, cancellationToken))
            throw AuthenticationExceptions.UserAlreadyExists;

        string hashedPassword = _passwordHasher.HashPassword(request.Password);

        var user = User.Create(request.Username, hashedPassword);

        string token = _jwtTokenGenerator.GenerateToken(user);
        var refreshToken = _refreshTokenGenerator.GenerateRefreshToken();

        user.SetRefreshToken(refreshToken.Token, refreshToken.ExpiryTime);

        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthenticationResult(token, refreshToken.Token, refreshToken.ExpiryTime);
    }

    public async Task<AuthenticationResult> LoginAsync(AuthenticationRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetUserByUsernameAsync(request.Username, cancellationToken);

        if (user is null)
            throw UserExceptions.UserNotFound;

        if (!_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            throw AuthenticationExceptions.IncorrectPassword;

        string token = _jwtTokenGenerator.GenerateToken(user);
        var refreshToken = _refreshTokenGenerator.GenerateRefreshToken();

        user.SetRefreshToken(refreshToken.Token, refreshToken.ExpiryTime);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new AuthenticationResult(token, refreshToken.Token, refreshToken.ExpiryTime);
    }

    public async Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenRequest request,
        CancellationToken cancellationToken = default)
    {
        ClaimsPrincipal principal;

        try
        {
            principal = _jwtTokenGenerator.GetPrincipalFromExpiredToken(request.ExpiredToken);
        }
        catch (Exception e)
        {
            throw AuthenticationExceptions.InvalidToken(e);
        }

        var userId = Guid.Parse(principal.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

        var user = await _userRepository.GetUserByIdAsync(userId, cancellationToken);

        if (user is null)
            throw UserExceptions.UserNotFound;

        if (user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            throw AuthenticationExceptions.InvalidRefreshToken;

        string token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(token, user.RefreshToken, user.RefreshTokenExpiryTime);
    }
}