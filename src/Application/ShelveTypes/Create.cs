using Application.Core;
using MediatR;
using FluentValidation;
using Persistence;
using Domain;

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

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var model = new ShelveType
            {
                ShelfId = Guid.NewGuid(),
                ShelfTag = request.ShelveType.ShelfTag
            };

            _context.ShelveTypes.Add(model);

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Fail to create Shelve Type");

            return Result<Unit>.Success(Unit.Value);
        }

    }

}
