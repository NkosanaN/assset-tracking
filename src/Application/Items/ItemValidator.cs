using Application.Contracts.Persistence;
using Domain;
using FluentValidation;

namespace Application.Items;

public class ItemValidator : AbstractValidator<Item>
{
    public ItemValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertyName} is Required")
            .MinimumLength(5).WithMessage("{PropertyName} cannot be less than 5")
            .MaximumLength(50).WithMessage("{PropertyName} must be fewer than 50");

        RuleFor(x => x.Description).NotEmpty().MinimumLength(5).MaximumLength(100);
        RuleFor(x => x.Serialno).NotEmpty().MinimumLength(5).MaximumLength(50);
        RuleFor(x => x.Cost).NotEmpty();
        RuleFor(x => x.Qty).NotEmpty();
        RuleFor(x => x.DatePurchased).NotEmpty();
        
    }
    
}
