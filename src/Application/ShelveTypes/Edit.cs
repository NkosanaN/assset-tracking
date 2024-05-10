using Application.Contracts.Persistence;
using Application.Core;
using AutoMapper;
using MediatR;
using Domain;
namespace Application.ShelveTypes;

public class Edit
{
    /*
     * Command don't return any thing
     */
    public class Command : IRequest<Result<Unit>>
    {
        public ShelveType? ShelveType { get; set; }
    }

    public class Handler(IShelveRepository context, IMapper mapper) : IRequestHandler<Command, Result<Unit>>
    {
        private readonly IShelveRepository _context = context;

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var shelve = await _context.GetByIdAsync(request.ShelveType!.ShelfId);

            if (shelve is null) return null!;

            shelve.ShelfTag = request.ShelveType.ShelfTag;

            var result = await _context.CreateAsync(shelve);

            if (!result) return Result<Unit>.Failure("Failed to update ShelveType");

            //Unit.Value is the same as return nothing as Command don't return anything
            return Result<Unit>.Success(Unit.Value);

        }
    }
}