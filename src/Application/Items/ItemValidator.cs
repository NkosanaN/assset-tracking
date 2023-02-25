using Domain;
using FluentValidation;

namespace Application.Items;

public class ItemValidator : AbstractValidator<Item>
{
    public ItemValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(5).MaximumLength(50);
        RuleFor(x => x.Description).NotEmpty().MinimumLength(5).MaximumLength(100);
        RuleFor(x => x.Serialno).NotEmpty().MinimumLength(5).MaximumLength(50);
        RuleFor(x => x.Cost).NotEmpty();
        RuleFor(x => x.Qty).NotEmpty();
        RuleFor(x => x.DatePurchased).NotEmpty();
        
    }
}
