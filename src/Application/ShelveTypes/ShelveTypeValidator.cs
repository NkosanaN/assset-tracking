using Domain;
using FluentValidation;

namespace Application.ShelveTypes;

public class ShelveTypeValidator : AbstractValidator<ShelveType>
{
    public ShelveTypeValidator()
    {
        RuleFor(x => x.ShelfTag).NotEmpty().MinimumLength(5).MaximumLength(50);
    }
}
