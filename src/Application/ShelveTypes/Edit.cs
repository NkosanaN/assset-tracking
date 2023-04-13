using Application.Core;
using AutoMapper;
using MediatR;
using Domain;
using Persistence;

namespace Application.ShelveTypes;

public class Edit
{
    /*
     * Command don't return any thing
     */
    public class Command : IRequest<Result<Unit>>
    {
        public ShelveType shelvetype { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var shelve = await _context.ShelveTypes.FindAsync(request.shelvetype.ShelfId);

            if (shelve is null) return null!;

            shelve.ShelfTag = request.shelvetype.ShelfTag;

            var result = await _context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Failed to update ShelveType");

            //Unit.Value is the same as return nothing as Command don't return anything
            return Result<Unit>.Success(Unit.Value);
            
        }
    }
}