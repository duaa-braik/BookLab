using BookLab.Application.Dtos.Book;
using FluentValidation;

namespace BookLab.Application.Validators.Book;

public class CreateBookValidator : AbstractValidator<CreateBookDto>
{
    public CreateBookValidator()
    {
        RuleFor(b => b.Title)
            .NotEmpty().WithMessage("{PropertyName} should not be empty")
            .NotNull().WithMessage("{PropertyName} should not be null");

        RuleFor(b => b.PublisherId)
            .NotEmpty().WithMessage("{PropertyName} should not be empty")
            .NotNull().WithMessage("{PropertyName} should not be null")
            .GreaterThan(0).WithMessage("{PropertyName} should be a positive number");

        RuleFor(b => b.Price)
            .NotEmpty().WithMessage("{PropertyName} should not be empty")
            .NotNull().WithMessage("{PropertyName} should not be null")
            .GreaterThan(0).WithMessage("{PropertyName} should be a positive number");

        RuleFor(b => b.ISBN)
            .NotNull().WithMessage("{PropertyName} should not be null")
            .NotEmpty().WithMessage("{PropertyName} should not be empty")
            .Length(16).WithMessage("{PropertyName} should be 13 characters");
    }
}
