using Application.Suppliers.Contracts;
using FluentValidation;

namespace Application.Supplier;

public class SupplierValidator : AbstractValidator<SupplierRequest>
{
    public SupplierValidator()
    {
        RuleFor(x => x.SupplierName).NotEmpty().MinimumLength(5).MaximumLength(50);
        RuleFor(x => x.SupplierDescription).NotEmpty().MinimumLength(5).MaximumLength(50);
  
    }
}
