using FluentValidation;

namespace ToDoList.Application.Models.Authentication;

public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
{
    public AuthenticationRequestValidator()
    {
        RuleFor(x => x.Username).MinimumLength(4).MaximumLength(20);
        RuleFor(x => x.Password).Matches(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$").WithMessage(
            "Password must contain at least 8 characters, one letter and one number.");
    }
}