using FluentValidation;

namespace Application.Departments;

public class DepartmentValidator : AbstractValidator<DepartmentDto>
{
	public DepartmentValidator()
	{
		RuleFor(x => x.Description).NotEmpty().MinimumLength(5).MaximumLength(50);
		RuleFor(x => x.DepartmentName).NotEmpty().MinimumLength(5).MaximumLength(50);
	}
}
