using Application.Suppliers;
using FluentValidation;

namespace Application.Supplier;

public class SupplierValidator : AbstractValidator<SupplierDto>
{
    public SupplierValidator()
    {
        RuleFor(x => x.SupplierName)
            .NotEmpty().WithMessage("{PropertyName} is Required")
            .MinimumLength(5).WithMessage("{PropertyName} cannot be less than 5")
            .MaximumLength(50).WithMessage("{PropertyName} must be fewer than 50");

        RuleFor(x => x.SupplierDescription)
            .NotEmpty().WithMessage("{PropertyName} is Required")
            .MinimumLength(5).WithMessage("{PropertyName} cannot be less than 5")
            .MaximumLength(50).WithMessage("{PropertyName} must be fewer than 50");

    }
}
