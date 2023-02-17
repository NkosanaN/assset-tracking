using Application.Core;
using MediatR;
using Domain;
using Persistence;

namespace Application.Items;

public class Details
{
    public class Query : IRequest<Result<Item>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<Item>>
    {
        private readonly DataContext _context;

        public Handler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<Item>> Handle(Query request, CancellationToken cancellationToken)
        {
            var item =  await _context.Items.FindAsync(request.Id);

            return Result<Item>.Success(item!);
        }

    }

}
