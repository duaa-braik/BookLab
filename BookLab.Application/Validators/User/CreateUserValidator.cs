using BookLab.API.Dtos;
using FluentValidation;

namespace BookLab.Application.Validators.User;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("Password field should not be empty")
            .NotNull().WithMessage("Password field is required")
            .MinimumLength(8).WithMessage("Password should be at least 8 characters")
            .MaximumLength(30).WithMessage("Password is too long");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email field should not be empty")
            .NotNull().WithMessage("Email field is required")
            .Matches("^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$")
            .WithMessage(e => $"The email address '{e.Email}' is not in a valid format");

        RuleFor(c => c.Role)
            .NotEmpty().WithMessage("Role should not be empty")
            .NotNull().WithMessage("Role field is required");
    }
}
