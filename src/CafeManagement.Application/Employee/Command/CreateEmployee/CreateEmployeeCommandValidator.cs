using FluentValidation;

namespace CafeManagement.Application.Dto;

public class CreateEmployeeCommandValidator  : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(100);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches(@"^[89]\d{7}$")
            .WithMessage("Phone number must start with 8 or 9 and have 8 digits");

        RuleFor(x => x.Gender)
            .IsInEnum()
            .WithMessage("Gender must be either Male or Female");
    }
}