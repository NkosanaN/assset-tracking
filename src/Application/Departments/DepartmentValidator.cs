using Application.Departments.Contracts;
using FluentValidation;

namespace Application.Departments;

public class DepartmentValidator : AbstractValidator<DepartmentRequest>
{
    public DepartmentValidator()
    {
        RuleFor(x => x.Description).NotEmpty().MinimumLength(5).MaximumLength(50);
        RuleFor(x => x.DepartmentName).NotEmpty().MinimumLength(5).MaximumLength(50);
    }
}
