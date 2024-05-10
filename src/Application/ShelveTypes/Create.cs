using Application.Core;
using MediatR;
using FluentValidation;
using Domain;
using Application.Contracts.Persistence;

namespace Application.ShelveTypes;

public class Create
{
    /*
     * Command don't return any thing
     */
    public class Command : IRequest<Result<Unit>>
    {
        public ShelveType ShelveType { get; set; } = new();
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.ShelveType).SetValidator(new ShelveTypeValidator());
        }
    }

    public class Handler(IShelveRepository context) : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IShelveRepository _context = context;

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var exist = await _context.IsShelveTypeUnique(request.ShelveType.ShelfTag) == false;

            if (exist)
            {
                return Result<Unit>.Failure("Tag already exist");
            }

            var model = new ShelveType
            {
                ShelfId = Guid.NewGuid(),
                ShelfTag = request.ShelveType.ShelfTag
            };

            var result = await _context.CreateAsync(model); ;

            if (!result) return Result<Unit>.Failure("Fail to create Shelve Type");

            return Result<Unit>.Success(Unit.Value);
        }

    }

}
