using Application.Core;
using MediatR;
using Domain;
using Application.Contracts.Persistence;

namespace Application.Items;

public class Details
{
    public class Query : IRequest<Result<Item>>
    {
        public Guid Id { get; set; }
    }

    public class Handler(IItemRepository context) : IRequestHandler<Query, Result<Item>>
    {
        private readonly IItemRepository _context = context;

        public async Task<Result<Item>> Handle(Query request, CancellationToken cancellationToken)
        {
            var item =  await _context.GetItemWithShelveById(request.Id);

            return Result<Item>.Success(item!);
        }

    }

}
