using FluentValidation;
using Aura.Application.DTOs;

namespace Aura.Application.Validators;

public class CreateStudentValidator : AbstractValidator<CreateStudentDto>
{
    public CreateStudentValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Phone).NotEmpty().MinimumLength(10);
        RuleFor(x => x.CurrentGrade).InclusiveBetween(0, 10); // Nota de 0 a 10
        RuleFor(x => x.Subject).NotEmpty();
    }
}